using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class FluentButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FluentButton), new PropertyMetadata(new CornerRadius(0)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        static FluentButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentButton), new FrameworkPropertyMetadata(typeof(FluentButton)));
        }
    }
}
