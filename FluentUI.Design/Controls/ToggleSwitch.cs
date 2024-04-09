using System.Windows;
using FluentUI.Design.Tools;

namespace FluentUI.Design.Controls
{
    [DependencyProperty<object>("Header")]
    [DependencyProperty<bool>("IsOn")]
    [DependencyProperty<object>("OffContent", defaultCode: "\"Off\"")]
    [DependencyProperty<object>("OnContent", defaultCode: "\"On\"")]
    public partial class ToggleSwitch : CaptureControl
    {
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        public ToggleSwitch()
        {
            this.AddPointerDownHandler((a, b) => CaptureMouse());
            this.AddPointerUpHandler((a, b) => { ReleaseMouseCapture(); IsOn = !IsOn; });
        }
    }
}
