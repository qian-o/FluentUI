using FluentUI.Demo.ViewModels;
using FluentUI.Design;
using FluentUI.Design.Controls;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : FluentPage
    {
        public Page2(Page2ViewModel viewModel)
        {
            Core.BindingViewModel(this, viewModel);

            InitializeComponent();
        }
    }
}
