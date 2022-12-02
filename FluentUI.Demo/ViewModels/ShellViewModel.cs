using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentUI.Demo.Models.Messages;
using FluentUI.Demo.Views;
using FluentUI.Design.Controls;
using FluentUI.Design.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace FluentUI.Demo.ViewModels
{
    public partial class ShellViewModel : BaseViewModel<ShellPage>
    {
        [ObservableProperty]
        private ObservableCollection<NavigationViewItem> pages = new();

        [ObservableProperty]
        private NavigationViewItem selected;

        public ShellViewModel()
        {
            Pages.Add(new NavigationViewItem
            {
                FontFamily = Application.Current.FindResource("FontFamilyIcon") as FontFamily,
                Icon = "\uF56E",
                Content = "Page1",
                Tag = App.GetService<Page1>()
            });
            Pages.Add(new NavigationViewItem
            {
                FontFamily = Application.Current.FindResource("FontFamilyIcon") as FontFamily,
                Icon = "\uF56E",
                Content = "Page2",
                Tag = App.GetService<Page2>()
            });

            Messenger.Register<NavigationPageMessage>(this, RegisterNavigationPageMessage);
        }

        [RelayCommand]
        private void Loaded()
        {
            Selected = Pages[0];
        }

        partial void OnSelectedChanged(NavigationViewItem value)
        {
            Page.FrameContent.PageContent = value.Tag as System.Windows.Controls.Page;
        }

        private void RegisterNavigationPageMessage(object recipient, NavigationPageMessage message)
        {
            if (Pages.FirstOrDefault(item => item.Tag.GetType() == message.Value) is NavigationViewItem navigationViewItem)
            {
                Selected = navigationViewItem;
            }
        }
    }
}
