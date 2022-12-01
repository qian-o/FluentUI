using CommunityToolkit.Mvvm.Input;
using FluentUI.Demo.Views;
using FluentUI.Design;
using FluentUI.Design.Enums;
using FluentUI.Design.Models;

namespace FluentUI.Demo.ViewModels
{
    public partial class Page2ViewModel : BaseViewModel<Page2>
    {
        [RelayCommand]
        public static void ToggleTheme() => Core.RequestedTheme = Core.RequestedTheme == ElementTheme.Light ? ElementTheme.Dark : ElementTheme.Light;
    }
}
