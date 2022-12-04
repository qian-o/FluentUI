using FluentUI.Design.Controls;
using System.Windows;

namespace FluentUI.Design.Models
{
    public class SelectItemChangedEventArgs : RoutedEventArgs
    {
        public bool IsSetting { get; }

        public NavigationViewItem OldViewItem { get; }

        public NavigationViewItem NewViewItem { get; }

        public SelectItemChangedEventArgs(bool isSetting, NavigationViewItem oldViewItem, NavigationViewItem newViewItem)
        {
            RoutedEvent = NavigationView.SelectItemChangedEvent;
            IsSetting = isSetting;
            OldViewItem = oldViewItem;
            NewViewItem = newViewItem;
        }
    }
}
