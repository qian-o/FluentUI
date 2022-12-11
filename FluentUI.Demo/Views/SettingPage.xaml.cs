using FluentUI.Demo.ViewModels;
using FluentUI.Design;
using FluentUI.Design.Controls;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : FluentPage
    {
        public SettingPage(SettingViewModel viewModel)
        {
            Core.BindingViewModel(this, viewModel);

            InitializeComponent();
        }
    }
}
