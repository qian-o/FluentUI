using FluentUI.Design.Models;
using FluentUI.Design.Tools;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace FluentUI.Design.Controls
{
    public class NavigationView : Control
    {
        #region Constant
        private const string Pane = "Pane";
        private const string FoldPane = "FoldPane";
        private const string SettingPane = "SettingPane";
        #endregion

        #region Variable
        private Border _pane;
        private NavigationViewItem _foldPane;
        private NavigationViewItem _settingPane;
        #endregion

        #region RoutedEvent
        public static readonly RoutedEvent SelectItemChangedEvent = EventManager.RegisterRoutedEvent(nameof(SelectItemChanged), RoutingStrategy.Direct, typeof(EventHandler<SelectItemChangedEventArgs>), typeof(NavigationView));

        public event EventHandler<SelectItemChangedEventArgs> SelectItemChanged
        {
            add => AddHandler(SelectItemChangedEvent, value);
            remove => RemoveHandler(SelectItemChangedEvent, value);
        }
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(NavigationView), new PropertyMetadata(null));
        public static readonly DependencyProperty IsPaneOpenProperty = DependencyProperty.Register(nameof(IsPaneOpen), typeof(bool), typeof(NavigationView), new PropertyMetadata(true, OnIsPaneOpenChanged));
        public static readonly DependencyProperty OpenPaneLengthProperty = DependencyProperty.Register(nameof(OpenPaneLength), typeof(double), typeof(NavigationView), new PropertyMetadata(256d));
        public static readonly DependencyProperty CompactPaneLengthProperty = DependencyProperty.Register(nameof(CompactPaneLength), typeof(double), typeof(NavigationView), new PropertyMetadata(48d));
        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(nameof(MenuItems), typeof(ObservableCollection<NavigationViewItem>), typeof(NavigationView), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectItemProperty = DependencyProperty.Register(nameof(SelectItem), typeof(NavigationViewItem), typeof(NavigationView), new PropertyMetadata(null, OnSelectItemChanged));
        public static readonly DependencyProperty IsSettingProperty = DependencyProperty.Register(nameof(IsSetting), typeof(bool), typeof(NavigationView), new PropertyMetadata(true));
        public static readonly DependencyProperty SettingContentProperty = DependencyProperty.Register(nameof(SettingContent), typeof(string), typeof(NavigationView), new PropertyMetadata("Setting"));

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
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
        public ObservableCollection<NavigationViewItem> MenuItems
        {
            get { return (ObservableCollection<NavigationViewItem>)GetValue(MenuItemsProperty); }
            set { SetValue(MenuItemsProperty, value); }
        }
        public NavigationViewItem SelectItem
        {
            get { return (NavigationViewItem)GetValue(SelectItemProperty); }
            set { SetValue(SelectItemProperty, value); }
        }
        public bool IsSetting
        {
            get { return (bool)GetValue(IsSettingProperty); }
            set { SetValue(IsSettingProperty, value); }
        }
        public string SettingContent
        {
            get { return (string)GetValue(SettingContentProperty); }
            set { SetValue(SettingContentProperty, value); }
        }
        #endregion

        static NavigationView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationView), new FrameworkPropertyMetadata(typeof(NavigationView)));
        }

        public NavigationView()
        {
            MenuItems = new ObservableCollection<NavigationViewItem>();
        }

        public override void OnApplyTemplate()
        {
            _pane = GetTemplateChild(Pane) as Border;
            _foldPane = GetTemplateChild(FoldPane) as NavigationViewItem;
            _settingPane = GetTemplateChild(SettingPane) as NavigationViewItem;

            SetPaneActualWidth();
            _foldPane.AddPointerUpHandler((a, b) => IsPaneOpen = !IsPaneOpen);
        }

        /// <summary>
        /// Operation width after state switching
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnIsPaneOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NavigationView navigationView)
            {
                navigationView.SetPaneActualWidth();
            }
        }

        /// <summary>
        /// Raises the SelectItemChanged routing event
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSelectItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NavigationView navigationView)
            {
                navigationView.RaiseEvent(new SelectItemChangedEventArgs(navigationView._settingPane == e.NewValue, (NavigationViewItem)e.OldValue, (NavigationViewItem)e.NewValue));
            }
        }

        /// <summary>
        /// Set the actual width of the Pane
        /// </summary>
        private void SetPaneActualWidth()
        {
            if (_pane != null)
            {
                double currentWidth = _pane.Width;
                double toWidth = IsPaneOpen ? OpenPaneLength : CompactPaneLength;
                if (double.IsNaN(currentWidth))
                {
                    _pane.SetBinding(WidthProperty, new Binding { Source = this, Path = new PropertyPath(IsPaneOpen ? nameof(OpenPaneLength) : nameof(CompactPaneLength)) });
                }
                else
                {
                    BindingOperations.ClearBinding(_pane, WidthProperty);
                    DoubleAnimation doubleAnimation = new()
                    {
                        From = currentWidth,
                        To = toWidth,
                        Duration = new TimeSpan(0, 0, 0, 0, 80)
                    };
                    doubleAnimation.Completed += delegate
                    {
                        _pane.SetBinding(WidthProperty, new Binding { Source = this, Path = new PropertyPath(IsPaneOpen ? nameof(OpenPaneLength) : nameof(CompactPaneLength)) });
                    };
                    _pane.BeginAnimation(WidthProperty, doubleAnimation);
                }
            }
        }
    }
}
