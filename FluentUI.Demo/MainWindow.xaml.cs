using FluentUI.Design.Controls;
using FluentUI.Design.Enums;
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
            if (RequestedTheme == ElementTheme.Light)
            {
                RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                RequestedTheme = ElementTheme.Light;
            }
        }
    }
}
