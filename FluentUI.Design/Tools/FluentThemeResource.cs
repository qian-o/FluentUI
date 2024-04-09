using System;
using System.ComponentModel;
using System.Windows;
using FluentUI.Design.Enums;

namespace FluentUI.Design.Tools
{
    public class FluentThemeResource : ResourceDictionary, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ElementTheme theme;

        public ElementTheme Theme
        {
            get => theme;
            set
            {
                theme = value;

                GenerateResource();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Theme)));
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
                Source = new Uri(@"pack://application:,,,/FluentUI.Design;component/Styles/FluentColor.xaml")
            };
            colorDictionary.MergedDictionaries.Clear();
            colorDictionary.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(@$"pack://application:,,,/FluentUI.Design;component/Styles/FluentColor.{Theme}.xaml")
            });

            MergedDictionaries.Add(colorDictionary);
        }
    }
}
