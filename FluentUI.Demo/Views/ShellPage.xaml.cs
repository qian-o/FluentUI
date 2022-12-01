using FluentUI.Demo.ViewModels;
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
    }
}
