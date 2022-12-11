using FluentUI.Demo.ViewModels;
using FluentUI.Design;
using FluentUI.Design.Controls;
using FluentUI.Design.Models;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// ShellPage.xaml 的交互逻辑
    /// </summary>
    public partial class ShellPage : FluentPage
    {
        public ShellPage(ShellViewModel viewModel)
        {
            Core.BindingViewModel(this, viewModel);

            InitializeComponent();
        }

        private void NavigationView_SelectItemChanged(object sender, SelectItemChangedEventArgs e)
        {
            (DataContext as ShellViewModel).SelectItemChangedCommand.Execute(e);
        }
    }
}
