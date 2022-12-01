using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    public class SplitView : Control
    {
        static SplitView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitView), new FrameworkPropertyMetadata(typeof(SplitView)));
        }
    }
}
