using FluentUI.Design.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FluentUI.Design.Controls
{
    public class FluentWindow : Window
    {
        #region 依赖属性
        public static readonly DependencyProperty RequestedThemeProperty = DependencyProperty.Register(nameof(RequestedTheme), typeof(ElementTheme), typeof(FluentWindow), new PropertyMetadata(ElementTheme.Light));

        public ElementTheme RequestedTheme
        {
            get => (ElementTheme)GetValue(RequestedThemeProperty);
            internal set => SetValue(RequestedThemeProperty, value);
        }
        #endregion

        static FluentWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentWindow), new FrameworkPropertyMetadata(typeof(FluentWindow)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

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
