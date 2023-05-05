using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    [DependencyProperty<CornerRadius>("CornerRadius")]
    public partial class FluentButton : Button
    {
        static FluentButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentButton), new FrameworkPropertyMetadata(typeof(FluentButton)));
        }
    }
}
