using FluentUI.Demo.ViewModels;
using FluentUI.Demo.Views;
using FluentUI.Design;
using FluentUI.Design.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Demo
{
    public partial class App : Application
    {
        public static new MainWindow MainWindow { get; } = new MainWindow();

        public IHost Host { get; }

        public App()
        {
            Host = Microsoft.Extensions.Hosting.Host.
               CreateDefaultBuilder().
               ConfigureServices(services =>
               {
                   services.AddTransient<ShellPage>();
                   services.AddSingleton<ShellViewModel>();
                   services.AddTransient<Page1>();
                   services.AddSingleton<Page1ViewModel>();
                   services.AddTransient<Page2>();
                   services.AddSingleton<Page2ViewModel>();
               }).
               Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Core.Initialize();

            MainWindow.Content = GetService<ShellPage>();
            MainWindow.Show();
        }

        public static T GetService<T>() where T : class
        {
            if ((Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }

            return service;
        }

        public static T GetViewModel<TPage, T>(TPage page) where TPage : Page where T : BaseViewModel<TPage>
        {
            T viewModel = GetService<T>();
            FieldInfo[] fieldInfos = typeof(BaseViewModel<TPage>).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo item in fieldInfos)
            {
                if (item.Name == $"<{nameof(BaseViewModel<TPage>.Page)}>k__BackingField")
                {
                    item.SetValue(viewModel, page);
                    break;
                }
            }

            return viewModel;
        }
    }
}
