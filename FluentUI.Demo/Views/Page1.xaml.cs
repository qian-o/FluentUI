using FluentUI.Demo.ViewModels;
using System.Windows.Controls;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

            DataContext = App.GetViewModel<Page1, Page1ViewModel>(this);
        }
    }
}
