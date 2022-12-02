using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FluentUI.Design.Controls
{
    public class FluentWindow : Window
    {
        static FluentWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentWindow), new FrameworkPropertyMetadata(typeof(FluentWindow)));
        }

        public override void OnApplyTemplate()
        {
            UniformGrid uniformGrid = GetTemplateChild("AeroCaptionButtons") as UniformGrid;
            foreach (object item in uniformGrid.Children)
            {
                if (item is Button button)
                {
                    button.Click += AeroCaption_Click;
                }
            }
        }

        private void AeroCaption_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Tag.ToString() == "Minimized")
            {
                WindowState = WindowState.Minimized;
            }
            else if (button.Tag.ToString() == "WindowState")
            {
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            }
            else
            {
                Close();
            }
        }
    }
}
