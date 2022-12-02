using FluentUI.Demo.ViewModels;
using System.Windows.Controls;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            DataContext = App.GetViewModel<Page2, Page2ViewModel>(this);

            InitializeComponent();
        }
    }
}
