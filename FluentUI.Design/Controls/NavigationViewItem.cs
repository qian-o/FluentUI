using FluentUI.Design.Converters;
using FluentUI.Design.Tools;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FluentUI.Design.Controls
{
    public class NavigationViewItem : Control
    {
        #region Constant
        private const string Text = "Text";
        private const string Items = "Items";
        private const string Arrow = "Arrow";
        private const string ItemsSelect = "ItemsSelect";
        private const double MinBody = 80;
        private const string Fold = "Fold";
        #endregion

        #region Variable
        private TextBlock _text;
        private ItemsControl _items;
        private TextBlock _arrow;
        private Border _itemsSelect;
        private NavigationView _parent;
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(object), typeof(NavigationViewItem), new PropertyMetadata(null));
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(string), typeof(NavigationViewItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(NavigationViewItem), new PropertyMetadata(false));
        public static readonly DependencyProperty MenuItemsAnyProperty = DependencyProperty.Register(nameof(MenuItemsAny), typeof(bool), typeof(NavigationViewItem), new PropertyMetadata(false));
        public static readonly DependencyProperty IsExpansionProperty = DependencyProperty.Register(nameof(IsExpansion), typeof(bool), typeof(NavigationViewItem), new PropertyMetadata(false));
        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(nameof(MenuItems), typeof(ObservableCollection<NavigationViewItem>), typeof(NavigationViewItem), new PropertyMetadata(null));
        public static readonly DependencyProperty ItemsMarginProperty = DependencyProperty.Register(nameof(ItemsMargin), typeof(Thickness), typeof(NavigationViewItem), new PropertyMetadata(new Thickness(0)));

        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        internal bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public bool MenuItemsAny
        {
            get { return (bool)GetValue(MenuItemsAnyProperty); }
            set { SetValue(MenuItemsAnyProperty, value); }
        }
        public bool IsExpansion
        {
            get { return (bool)GetValue(IsExpansionProperty); }
            set { SetValue(IsExpansionProperty, value); }
        }
        public ObservableCollection<NavigationViewItem> MenuItems
        {
            get { return (ObservableCollection<NavigationViewItem>)GetValue(MenuItemsProperty); }
            set { SetValue(MenuItemsProperty, value); }
        }
        public Thickness ItemsMargin
        {
            get { return (Thickness)GetValue(ItemsMarginProperty); }
            set { SetValue(ItemsMarginProperty, value); }
        }
        #endregion

        static NavigationViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationViewItem), new FrameworkPropertyMetadata(typeof(NavigationViewItem)));
        }

        public NavigationViewItem()
        {
            MenuItems = new ObservableCollection<NavigationViewItem>();
            MenuItems.CollectionChanged += MenuItems_CollectionChanged;
        }

        public override void OnApplyTemplate()
        {
            _text = GetTemplateChild(Text) as TextBlock;
            _items = GetTemplateChild(Items) as ItemsControl;
            _arrow = GetTemplateChild(Arrow) as TextBlock;
            _itemsSelect = GetTemplateChild(ItemsSelect) as Border;

            if (GetParentNavigationView() is NavigationView navigationView)
            {
                _parent = navigationView;
                if (Tag?.ToString() != Fold)
                {
                    this.AddPointerUpHandler(NavigationViewItem_PointerUp);

                    SetBinding(IsSelectedProperty, new Binding
                    {
                        Source = navigationView,
                        Path = new PropertyPath(nameof(NavigationView.SelectItem)),
                        Converter = new MatchObjectConvert(),
                        ConverterParameter = this
                    });
                    _itemsSelect.SetBinding(VisibilityProperty, new Binding
                    {
                        Source = navigationView,
                        Path = new PropertyPath(nameof(NavigationView.SelectItem)),
                        Converter = new NavigationViewItemMenuItemsSelectedConvert(),
                        ConverterParameter = this
                    });
                }
            }
            UpdateDisplayMode();
        }

        /// <summary>
        /// Monitor size and update display mode
        /// </summary>
        /// <param name="sizeInfo"></param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) => UpdateDisplayMode();

        /// <summary>
        /// Click Event for Process Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationViewItem_PointerUp(object sender, RoutedEventArgs e)
        {
            if (!e.Handled)
            {
                _parent.SelectItem = this;
                if (MenuItemsAny)
                {
                    IsExpansion = !IsExpansion;
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// Process Child Margin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            MenuItemsAny = MenuItems.Any();
            foreach (NavigationViewItem item in MenuItems)
            {
                item.ItemsMargin = new Thickness(30, 0, 0, 0);
            }
        }

        /// <summary>
        /// Update display mode
        /// </summary>
        private void UpdateDisplayMode()
        {
            if (_text != null)
            {
                DockPanel dockPanel = _text.Parent as DockPanel;
                if (ActualWidth < MinBody)
                {
                    _text.Visibility = Visibility.Collapsed;
                    _items.Visibility = Visibility.Collapsed;
                    if (MenuItemsAny)
                    {
                        _arrow.Visibility = Visibility.Collapsed;
                    }

                    dockPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    dockPanel.Margin = new Thickness(0);
                }
                else
                {
                    _text.Visibility = Visibility.Visible;
                    _items.Visibility = Visibility.Visible;
                    if (MenuItemsAny)
                    {
                        _arrow.Visibility = Visibility.Visible;
                    }

                    dockPanel.HorizontalAlignment = HorizontalAlignment.Left;
                    dockPanel.Margin = new Thickness(13, 0, 0, 0);
                }
            }
        }

        /// <summary>
        /// Get Parent NavigationView
        /// </summary>
        /// <returns></returns>
        private NavigationView GetParentNavigationView()
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (parent?.GetType() != typeof(NavigationView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as NavigationView;
        }
    }
}
