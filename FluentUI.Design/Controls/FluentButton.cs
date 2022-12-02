using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class FluentButton : Button
    {
        #region DependencyProperty
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(FluentButton), new PropertyMetadata(new CornerRadius(0)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        static FluentButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentButton), new FrameworkPropertyMetadata(typeof(FluentButton)));
        }
    }
}
