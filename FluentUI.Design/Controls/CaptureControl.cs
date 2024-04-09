using System.Windows.Controls;
using System.Windows.Input;
using FluentUI.Design.Tools;

namespace FluentUI.Design.Controls;

public class CaptureControl : Control
{
    public CaptureControl()
    {
        Cursor = Cursors.Hand;

        this.AddPointerDownHandler((a, b) => CaptureMouse());
        this.AddPointerUpHandler((a, b) => ReleaseMouseCapture());
    }
}
