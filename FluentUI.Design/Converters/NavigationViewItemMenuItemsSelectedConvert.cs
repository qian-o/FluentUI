using FluentUI.Design.Controls;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FluentUI.Design.Converters
{
    public class NavigationViewItemMenuItemsSelectedConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is NavigationViewItem selected && parameter is NavigationViewItem navigationViewItem)
            {
                return navigationViewItem.MenuItems.Contains(selected) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
