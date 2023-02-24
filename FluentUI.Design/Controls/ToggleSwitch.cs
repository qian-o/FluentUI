using FluentUI.Design.Tools;
using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class ToggleSwitch : Control
    {
        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty IsOnProperty;
        public static readonly DependencyProperty OffContentProperty;
        public static readonly DependencyProperty OnContentProperty;

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
            HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(object), typeof(ToggleSwitch), new PropertyMetadata(null));
            IsOnProperty = DependencyProperty.Register(nameof(IsOn), typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(false));
            OffContentProperty = DependencyProperty.Register(nameof(OffContent), typeof(object), typeof(ToggleSwitch), new PropertyMetadata("Off"));
            OnContentProperty = DependencyProperty.Register(nameof(OnContent), typeof(object), typeof(ToggleSwitch), new PropertyMetadata("On"));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        public ToggleSwitch()
        {
            this.AddPointerUpHandler((a, b) => IsOn = !IsOn);
        }
    }
}
