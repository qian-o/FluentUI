using FluentUI.Design.Tools;
using System.Windows.Controls;
using System.Windows.Input;

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
