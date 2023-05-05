using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentUI.CodeGenerator
{
    [Generator]
    public class DependencyPropertyCodeGenerator : ISourceGenerator
    {
        private const string AttributeNamespace = "FluentUI";
        private const string AttributeClass = "DependencyPropertyAttribute";
        private const string AttributeText = @"
using System;

namespace FluentUI
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal class DependencyPropertyAttribute<T> : Attribute
    {
        public DependencyPropertyAttribute(string name, string defaultCode = null, bool isNullable = false)
        {
        }
    }
}";
        private static readonly DiagnosticDescriptor PartialWarning = new DiagnosticDescriptor("DP_001",
                                                                                               "DependencyPropertyCodeGenerator",
                                                                                               "'{0}' must be set to partial",
                                                                                               "Design",
                                                                                               DiagnosticSeverity.Warning,
                                                                                               true);
        private static readonly DiagnosticDescriptor DependencyObjectWarning = new DiagnosticDescriptor("DP_002",
                                                                                                        "DependencyPropertyCodeGenerator",
                                                                                                        "'{0}' must be a derived class of DependencyObject",
                                                                                                        "Design",
                                                                                                        DiagnosticSeverity.Warning,
                                                                                                        true);
        private static readonly DiagnosticDescriptor StaticWarning = new DiagnosticDescriptor("DP_003",
                                                                                              "DependencyPropertyCodeGenerator",
                                                                                              "'{0}' cannot be a static class",
                                                                                              "Design",
                                                                                              DiagnosticSeverity.Warning,
                                                                                              true);

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForPostInitialization((i) =>
            {
                i.AddSource($"{AttributeClass}.g.cs", AttributeText);
            });

            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is SyntaxReceiver receiver)
            {
                foreach (ITypeSymbol item in receiver.Classes)
                {
                    try
                    {
                        context.AddSource($"{item.ContainingNamespace.ToDisplayString()}_{item.Name}.DependencyProperty.g.cs", ProcessClass(item));
                    }
                    catch (PartialException pe)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(PartialWarning, Location.None, pe.Message));
                        continue;
                    }
                    catch (DependencyObjectException de)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(DependencyObjectWarning, Location.None, de.Message));
                        continue;
                    }
                    catch (StaticException se)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(StaticWarning, Location.None, se.Message));
                        continue;
                    }
                }
            }
        }

        private string ProcessClass(ITypeSymbol classSymbol)
        {
            string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

            bool isStatic = false;
            bool isPartial = false;
            foreach (SyntaxNode item in classSymbol.DeclaringSyntaxReferences.Select(r => r.GetSyntax()))
            {
                if (item is ClassDeclarationSyntax syntax)
                {
                    if (syntax.Modifiers.Any(SyntaxKind.StaticKeyword))
                    {
                        isStatic = true;
                    }

                    if (syntax.Modifiers.Any(SyntaxKind.PartialKeyword))
                    {
                        isPartial = true;
                    }
                }
            }

            if (isStatic)
            {
                throw new StaticException(classSymbol.ToDisplayString());
            }

            if (!isPartial)
            {
                throw new PartialException(classSymbol.ToDisplayString());
            }

            bool isDependencyObject = false;
            string namespaceDependencyObject = string.Empty;

            INamedTypeSymbol baseType = classSymbol.BaseType;
            while (baseType != null)
            {
                if (baseType.Name == "DependencyObject")
                {
                    namespaceDependencyObject = baseType.ContainingNamespace.ToDisplayString();

                    if (namespaceDependencyObject == "System.Windows")
                    {
                        isDependencyObject = true;

                        break;
                    }
                }
                baseType = baseType.BaseType;
            }

            if (!isDependencyObject)
            {
                throw new DependencyObjectException(classSymbol.ToDisplayString());
            }

            StringBuilder source = new StringBuilder();
            source.AppendLine($@"namespace {namespaceName}");
            source.AppendLine($@"{{");
            source.AppendLine($@"   using {namespaceDependencyObject};");
            source.AppendLine();
            source.AppendLine($@"   partial class {classSymbol.Name}");
            source.AppendLine($@"   {{");

            List<AttributeData> attributes = classSymbol.GetAttributes().Where(item => item.AttributeClass.ToDisplayString().Contains($"{AttributeNamespace}.{AttributeClass}")).ToList();
            foreach (AttributeData item in attributes)
            {
                ITypeSymbol typeSymbol = item.AttributeClass.TypeArguments.Single();
                TypedConstant nameConstant = item.ConstructorArguments[0];
                TypedConstant defaultCodeConstant = item.ConstructorArguments[1];
                TypedConstant isNullableConstant = item.ConstructorArguments[2];

                string code;
                if (defaultCodeConstant.Value != null)
                {
                    code = (string)defaultCodeConstant.Value;
                }
                else
                {
                    code = $"default({typeSymbol})";
                }

                ProcessProperty(source, classSymbol, typeSymbol, (string)nameConstant.Value, code, (bool)isNullableConstant.Value);

                if (item != attributes.Last())
                {
                    source.AppendLine();
                }
            }

            source.AppendLine($@"   }}");
            source.AppendLine($@"}}");

            return source.ToString();
        }

        private void ProcessProperty(StringBuilder source, ITypeSymbol classSymbol, ITypeSymbol typeSymbol, string name, string defaultCode, bool isNullable)
        {
            string type = typeSymbol.ToString();
            if (isNullable)
            {
                type += "?";
            }

            source.AppendLine($@"       public {type} {name}");
            source.AppendLine($@"       {{");
            source.AppendLine($@"           get {{ return ({type})GetValue({name}Property); }}");
            source.AppendLine($@"           set {{ SetValue({name}Property, value); }}");
            source.AppendLine($@"       }}");
            source.AppendLine();
            source.AppendLine($@"       public static readonly DependencyProperty {name}Property = DependencyProperty.Register(nameof({name}), typeof({type}), typeof({classSymbol.Name}), new PropertyMetadata({defaultCode}, (a, b) =>");
            source.AppendLine($@"       {{");
            source.AppendLine($@"           (({classSymbol.Name})a).On{name}Changed(({type})b.OldValue, ({type})b.NewValue);");
            source.AppendLine($@"       }}));");
            source.AppendLine();
            source.AppendLine($@"       partial void On{name}Changed({type} oldValue, {type} newValue);");
        }

        class SyntaxReceiver : ISyntaxContextReceiver
        {
            public List<ITypeSymbol> Classes { get; } = new List<ITypeSymbol>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is ClassDeclarationSyntax classDeclarationSyntax && classDeclarationSyntax.AttributeLists.Count > 0)
                {
                    ITypeSymbol typeSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax) as ITypeSymbol;
                    if (typeSymbol.GetAttributes().Any(ad => ad.AttributeClass.ToDisplayString().Contains($"{AttributeNamespace}.{AttributeClass}")))
                    {
                        Classes.Add(typeSymbol);
                    }
                }
            }
        }

        class PartialException : Exception
        {
            public PartialException(string className) : base(className)
            {
            }
        }

        class DependencyObjectException : Exception
        {
            public DependencyObjectException(string className) : base(className)
            {
            }
        }

        class StaticException : Exception
        {
            public StaticException(string className) : base(className)
            {
            }
        }
    }
}
