using FluentUI.Demo.ViewModels;
using System.Windows.Controls;

namespace FluentUI.Demo.Views
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            DataContext = App.GetViewModel<SettingPage, SettingViewModel>(this);

            InitializeComponent();
        }
    }
}
