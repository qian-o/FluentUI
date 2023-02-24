using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FluentUI.Design.Controls
{
    public class FluentFrame : Control
    {
        #region Constant
        private const string FrameContent = "FrameContent";
        private const string ExcessivePageHide = "ExcessivePageHide";
        private const string ExcessivePageShow = "ExcessivePageShow";
        #endregion

        #region Variable
        private Frame _frameContent;
        private Storyboard _excessivePageHide;
        private Storyboard _excessivePageShow;
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty PageContentProperty;

        public Page PageContent
        {
            get { return (Page)GetValue(PageContentProperty); }
            set { SetValue(PageContentProperty, value); }
        }
        #endregion

        static FluentFrame()
        {
            PageContentProperty = DependencyProperty.Register(nameof(PageContent), typeof(Page), typeof(FluentFrame), new PropertyMetadata(null, OnPageContentChanged));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentFrame), new FrameworkPropertyMetadata(typeof(FluentFrame)));
        }

        public override void OnApplyTemplate()
        {
            _frameContent = GetTemplateChild(FrameContent) as Frame;
            _excessivePageHide = _frameContent.Resources[ExcessivePageHide] as Storyboard;
            _excessivePageShow = _frameContent.Resources[ExcessivePageShow] as Storyboard;

            NavigatePage();
        }

        private static void OnPageContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FluentFrame fluentFrame)
            {
                fluentFrame.NavigatePage();
            }
        }

        /// <summary>
        /// Navigate to the specified page
        /// </summary>
        private async void NavigatePage()
        {
            if (_frameContent != null && PageContent != null)
            {
                _excessivePageHide.Begin(_frameContent);
                await Task.Delay(150);
                _frameContent.Navigate(PageContent);
                _excessivePageShow.Begin(_frameContent);
            }
        }
    }
}
