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
    [DependencyProperty<object>("Content")]
    [DependencyProperty<bool>("IsPaneOpen", defaultCode: "true")]
    [DependencyProperty<double>("OpenPaneLength", defaultCode: "256d")]
    [DependencyProperty<double>("CompactPaneLength", defaultCode: "48d")]
    [DependencyProperty<ObservableCollection<NavigationViewItem>>("MenuItems")]
    [DependencyProperty<NavigationViewItem>("SelectItem")]
    [DependencyProperty<bool>("IsSetting", defaultCode: "true")]
    [DependencyProperty<string>("SettingContent", defaultCode: "\"Setting\"")]
    public partial class NavigationView : Control
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
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        partial void OnIsPaneOpenChanged(bool oldValue, bool newValue)
        {
            SetPaneActualWidth();
        }

        /// <summary>
        /// Raises the SelectItemChanged routing event
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        partial void OnSelectItemChanged(NavigationViewItem oldValue, NavigationViewItem newValue)
        {
            RaiseEvent(new SelectItemChangedEventArgs(_settingPane == newValue, oldValue, newValue));
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
