using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentUI.Demo.Models.Messages;
using FluentUI.Demo.Views;
using FluentUI.Design;
using FluentUI.Design.Controls;
using FluentUI.Design.Models;

namespace FluentUI.Demo.ViewModels
{
    public partial class ShellViewModel : BaseViewModel<ShellPage>
    {
        [ObservableProperty]
        private ObservableCollection<NavigationViewItem> pages = [];

        [ObservableProperty]
        private NavigationViewItem selected;

        public ShellViewModel()
        {
            Pages.Add(new()
            {
                FontFamily = Application.Current.FindResource("FontFamilyIcon") as FontFamily,
                Icon = "\uF56E",
                Content = "Page1",
                Tag = Core.GetService<Page1>()
            });
            Pages.Add(new()
            {
                FontFamily = Application.Current.FindResource("FontFamilyIcon") as FontFamily,
                Icon = "\uF56E",
                Content = "Page2",
                Tag = Core.GetService<Page2>()
            });
            NavigationViewItem group = new()
            {
                IsGroup = true,
                FontFamily = Application.Current.FindResource("FontFamilyIcon") as FontFamily,
                Icon = "\uF168",
                Content = "Group"
            };
            group.MenuItems.Add(new()
            {
                FontFamily = Application.Current.FindResource("FontFamilyIcon") as FontFamily,
                Icon = "\uE722",
                Content = "Camera",
                Tag = new FluentPage
                {
                    Content = new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Style = Application.Current.FindResource("Display") as Style,
                        Text = "Camera"
                    }
                }
            });
            group.MenuItems.Add(new()
            {
                FontFamily = Application.Current.FindResource("FontFamilyIcon") as FontFamily,
                Icon = "\uE715",
                Content = "Mail",
                Tag = new FluentPage
                {
                    Content = new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Style = Application.Current.FindResource("Display") as Style,
                        Text = "Mail"
                    }
                }
            });
            Pages.Add(group);
            Pages.Add(new()
            {
                FontFamily = Application.Current.FindResource("FontFamilyIcon") as FontFamily,
                Icon = "\uF56E",
                Content = "Page3",
                Tag = new FluentPage
                {
                    Content = new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Style = Application.Current.FindResource("Display") as Style,
                        Text = "Page3"
                    }
                }
            });

            Messenger.Register<NavigationPageMessage>(this, RegisterNavigationPageMessage);
        }

        [RelayCommand]
        private void Loaded()
        {
            Selected = Pages[0];
        }

        [RelayCommand]
        private void SelectItemChanged(SelectItemChangedEventArgs e)
        {
            if (e.IsSetting)
            {
                Page.FrameContent.PageContent = Core.GetService<SettingPage>();
            }
            else
            {
                Page.FrameContent.PageContent = e.NewViewItem.Tag as Page;
            }
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
