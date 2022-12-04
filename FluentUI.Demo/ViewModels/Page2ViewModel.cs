using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentUI.Demo.Models.Messages;
using FluentUI.Demo.Views;
using FluentUI.Design.Models;

namespace FluentUI.Demo.ViewModels
{
    public partial class Page2ViewModel : BaseViewModel<Page2>
    {
        [RelayCommand]
        private void GoToPage1()
        {
            Messenger.Send(new NavigationPageMessage(typeof(Page1)));
        }
    }
}
