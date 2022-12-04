using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class FluentPage : Page
    {
        static FluentPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentPage), new FrameworkPropertyMetadata(typeof(FluentPage)));
        }
    }
}
