using System;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentUI.Demo.Models.Messages;
using FluentUI.Demo.Views;
using FluentUI.Design.Models;

namespace FluentUI.Demo.ViewModels
{
    public partial class Page2ViewModel : BaseViewModel<Page2>
    {
        [ObservableProperty]
        private string lottie1;

        [ObservableProperty]
        private string lottie2;

        public Page2ViewModel()
        {
            Lottie1 = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Files", "120582-gdsc-modules.json");
            Lottie2 = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Files", "91881-design.json");
        }

        [RelayCommand]
        private void GoToPage1()
        {
            Messenger.Send(new NavigationPageMessage(typeof(Page1)));
        }
    }
}
