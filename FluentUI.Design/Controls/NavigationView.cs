using FluentUI.Design.Enums;
using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class NavigationView : Control
    {
        #region DependencyProperty
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(FrameworkElement), typeof(NavigationView), new PropertyMetadata(null));
        public static readonly DependencyProperty IsPaneOpenProperty = DependencyProperty.Register(nameof(IsPaneOpen), typeof(bool), typeof(NavigationView), new PropertyMetadata(false));
        public static readonly DependencyProperty DisplayModeProperty = DependencyProperty.Register(nameof(DisplayMode), typeof(NavigationViewDisplayMode), typeof(NavigationView), new PropertyMetadata(NavigationViewDisplayMode.Overlay));
        public static readonly DependencyProperty OpenPaneLengthProperty = DependencyProperty.Register(nameof(OpenPaneLength), typeof(double), typeof(NavigationView), new PropertyMetadata(256d));
        public static readonly DependencyProperty CompactPaneLengthProperty = DependencyProperty.Register(nameof(CompactPaneLength), typeof(double), typeof(NavigationView), new PropertyMetadata(48d));

        public FrameworkElement Content
        {
            get { return (FrameworkElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }
        public NavigationViewDisplayMode DisplayMode
        {
            get { return (NavigationViewDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }
        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }
        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }
        #endregion

        static NavigationView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationView), new FrameworkPropertyMetadata(typeof(NavigationView)));
        }
    }
}
