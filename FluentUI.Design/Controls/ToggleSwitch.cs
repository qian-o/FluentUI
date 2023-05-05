using FluentUI.Design.Tools;
using System.Windows;
using System.Windows.Controls;

namespace FluentUI.Design.Controls
{
    [DependencyProperty<object>("Header")]
    [DependencyProperty<bool>("IsOn")]
    [DependencyProperty<object>("OffContent", defaultCode: "\"Off\"")]
    [DependencyProperty<object>("OnContent", defaultCode: "\"On\"")]
    public partial class ToggleSwitch : Control
    {
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        public ToggleSwitch()
        {
            this.AddPointerUpHandler((a, b) => IsOn = !IsOn);
        }
    }
}
