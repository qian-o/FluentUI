using FluentUI.Demo.ViewModels;
using FluentUI.Design.Controls;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : FluentPage
    {
        public SettingPage()
        {
            DataContext = App.GetViewModel<SettingPage, SettingViewModel>(this);

            InitializeComponent();
        }
    }
}
