using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class FluentFrame : Control
    {
        #region Constant
        private const string FrameContent = "FrameContent";
        #endregion

        #region Variable
        private Frame _frameContent;
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty PageContentProperty = DependencyProperty.Register(nameof(PageContent), typeof(Page), typeof(FluentFrame), new PropertyMetadata(null, OnPageContentChanged));

        public Page PageContent
        {
            get { return (Page)GetValue(PageContentProperty); }
            set { SetValue(PageContentProperty, value); }
        }
        #endregion

        static FluentFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentFrame), new FrameworkPropertyMetadata(typeof(FluentFrame)));
        }

        public override void OnApplyTemplate()
        {
            _frameContent = GetTemplateChild(FrameContent) as Frame;

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
        private void NavigatePage()
        {
            if (_frameContent != null && PageContent != null)
            {
                _frameContent.Navigate(PageContent);
            }
        }
    }
}
