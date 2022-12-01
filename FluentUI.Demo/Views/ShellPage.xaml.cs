using FluentUI.Demo.ViewModels;
using FluentUI.Design;
using FluentUI.Design.Enums;
using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// ShellPage.xaml 的交互逻辑
    /// </summary>
    public partial class ShellPage : Page
    {
        public ShellPage()
        {
            InitializeComponent();

            DataContext = App.GetViewModel<ShellPage, ShellViewModel>(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Core.RequestedTheme == ElementTheme.Light)
            {
                Core.RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                Core.RequestedTheme = ElementTheme.Light;
            }
        }
    }
}
