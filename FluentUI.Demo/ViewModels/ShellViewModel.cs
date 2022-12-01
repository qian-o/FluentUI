using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentUI.Demo.Models;
using FluentUI.Demo.Models.Messages;
using FluentUI.Demo.Views;
using FluentUI.Design.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace FluentUI.Demo.ViewModels
{
    public partial class ShellViewModel : BaseViewModel<ShellPage>
    {
        [ObservableProperty]
        private ObservableCollection<PageModel> pages = new();

        [ObservableProperty]
        private PageModel select;

        public ShellViewModel()
        {
            pages.Add(new PageModel
            {
                Icon = "📄",
                Page = App.GetService<Page1>()
            });
            pages.Add(new PageModel
            {
                Icon = "📄",
                Page = App.GetService<Page2>()
            });

            Messenger.Register<NavigationPageMessage>(this, RegisterNavigationPageMessage);
        }

        [RelayCommand]
        private void Loaded()
        {
            SelectPage(pages[0]);
        }

        [RelayCommand]
        private void SelectPage(PageModel pageModel)
        {
            Select = pageModel;

            foreach (PageModel item in Pages)
            {
                item.IsSelect = false;
            }
            Select.IsSelect = true;

            Page.Body.Navigate(Select.Page);
        }

        private void RegisterNavigationPageMessage(object recipient, NavigationPageMessage message)
        {
            if (Pages.FirstOrDefault(item => item.Page.GetType() == message.Value) is PageModel pageModel)
            {
                SelectPage(pageModel);
            }
        }
    }
}
