using FluentUI.Demo.ViewModels;
using FluentUI.Design.Controls;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : FluentPage
    {
        public Page1()
        {
            DataContext = App.GetViewModel<Page1, Page1ViewModel>(this);

            InitializeComponent();
        }
    }
}
