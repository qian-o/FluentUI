using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class ToggleSwitch : Control
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(object), typeof(ToggleSwitch), new PropertyMetadata(null));
        public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register(nameof(IsOn), typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(false));
        public static readonly DependencyProperty OffContentProperty = DependencyProperty.Register(nameof(OffContent), typeof(object), typeof(ToggleSwitch), new PropertyMetadata(null));
        public static readonly DependencyProperty OnContentProperty = DependencyProperty.Register(nameof(OnContent), typeof(object), typeof(ToggleSwitch), new PropertyMetadata(null));

        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        public object OffContent
        {
            get { return GetValue(OffContentProperty); }
            set { SetValue(OffContentProperty, value); }
        }

        public object OnContent
        {
            get { return GetValue(OnContentProperty); }
            set { SetValue(OnContentProperty, value); }
        }

        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }
    }
}
