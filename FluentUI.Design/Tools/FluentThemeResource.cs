using FluentUI.Design.Enums;
using System;
using System.Windows;

namespace FluentUI.Design.Tools
{
    public class FluentThemeResource : ResourceDictionary
    {
        private ElementTheme theme;

        public ElementTheme Theme
        {
            get => theme;
            set
            {
                theme = value;
                GenerateResource();
            }
        }

        public FluentThemeResource()
        {
            GenerateResource();
        }

        private void GenerateResource()
        {
            MergedDictionaries.Clear();

            ResourceDictionary colorDictionary = new()
            {
                Source = new Uri(@"pack://application:,,,/FluentUI.Design;component/Styles/FluentColors.xaml")
            };
            colorDictionary.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(@$"pack://application:,,,/FluentUI.Design;component/Styles/FluentColors.{Theme}.xaml")
            });

            MergedDictionaries.Add(colorDictionary);
            MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(@"pack://application:,,,/FluentUI.Design;component/Styles/FluentTypography.xaml")
            });
        }
    }
}
