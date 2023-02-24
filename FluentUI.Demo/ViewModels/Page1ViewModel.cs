using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentUI.Demo.Models.Messages;
using FluentUI.Demo.Views;
using FluentUI.Design.Models;
using System.Windows.Media.Animation;

namespace FluentUI.Demo.ViewModels
{
    public partial class Page1ViewModel : BaseViewModel<Page1>
    {
        [ObservableProperty]
        private int clickCount;

        [ObservableProperty]
        private Storyboard storyboard;

        [RelayCommand]
        private void Loaded()
        {
            if (Storyboard == null)
            {
                Storyboard = Page.Resources["Revolve"] as Storyboard;
                Storyboard.Begin();
            }
        }

        [RelayCommand]
        private void ClickCountAdd()
        {
            ClickCount++;
        }

        [RelayCommand]
        private void GoToPage2()
        {
            Messenger.Send(new NavigationPageMessage(typeof(Page2)));
        }

        partial void OnClickCountChanged(int value)
        {
            if (ClickCount % 2 == 0)
            {
                Storyboard.Resume();
            }
            else
            {
                Storyboard.Pause();
            }
        }
    }
}
