using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class FluentScrollViewer : ScrollViewer
    {
        static FluentScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentScrollViewer), new FrameworkPropertyMetadata(typeof(FluentScrollViewer)));
        }
    }
}