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
            if (Navigation.SelectItem == null)
            {
                Navigation.SelectItem = Navigation.MenuItems[0];
            }
            else
            {
                Navigation.SelectItem = null;
            }
        }
    }
}
