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

            if (Theme == ElementTheme.Light)
            {
                MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri(@"pack://application:,,,/FluentUI.Design;component/Styles/FluentColors.Light.xaml")
                });
            }
            else
            {
                MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri(@"pack://application:,,,/FluentUI.Design;component/Styles/FluentColors.Dark.xaml")
                });
            }
            MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(@"pack://application:,,,/FluentUI.Design;component/Styles/FluentColors.xaml")
            });
            MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(@"pack://application:,,,/FluentUI.Design;component/Styles/FluentTypography.xaml")
            });
        }
    }
}
