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
    [DependencyProperty<object>("Icon")]
    [DependencyProperty<string>("Content")]
    [DependencyProperty<bool>("IsSelected")]
    [DependencyProperty<bool>("MenuItemsAny")]
    [DependencyProperty<bool>("IsExpansion")]
    [DependencyProperty<ObservableCollection<NavigationViewItem>>("MenuItems")]
    [DependencyProperty<Thickness>("ItemsMargin")]
    [DependencyProperty<bool>("IsGroup")]
    public partial class NavigationViewItem : Control
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
                if (!IsGroup)
                {
                    _parent.SelectItem = this;
                }
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