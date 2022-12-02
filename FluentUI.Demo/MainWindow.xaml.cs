using FluentUI.Design;
using FluentUI.Design.Controls;
using System.Windows;

namespace FluentUI.Demo
{
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Core.RequestedTheme == Design.Enums.ElementTheme.Light)
            {
                Core.RequestedTheme = Design.Enums.ElementTheme.Dark;
            }
            else
            {
                Core.RequestedTheme = Design.Enums.ElementTheme.Light;
            }
        }
    }
}
