using FluentUI.Demo.ViewModels;
using FluentUI.Design.Models;
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
            DataContext = App.GetViewModel<ShellPage, ShellViewModel>(this);

            InitializeComponent();
        }

        private void NavigationView_SelectItemChanged(object sender, SelectItemChangedEventArgs e)
        {
            (DataContext as ShellViewModel).SelectItemChangedCommand.Execute(e);
        }
    }
}
