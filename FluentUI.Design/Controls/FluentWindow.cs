using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace FluentUI.Design.Controls
{
    public class FluentWindow : Window
    {
        #region Constant
        private const string MinButton = "MinButton";
        private const string MaxButton = "MaxButton";
        private const string CloseButton = "CloseButton";
        private const uint WM_NCHITTEST = 0x0084;
        private const uint WM_NCLBUTTONDOWN = 0x00A1;
        private const uint WM_NCLBUTTONUP = 0x00A2;
        private const uint HT_CLIENT = 1;
        private const uint HT_MINBUTTON = 8;
        private const uint HT_MAXBUTTON = 9;
        private const uint HT_CLOSE = 20;
        private const string MouseOver = "MouseOver";
        private const string MouseCaptured = "MouseCaptured";
        #endregion

        #region Variable
        private Button _minButton;
        private Button _maxButton;
        private Button _closeButton;
        private IntPtr _windowHandle;
        private HwndSource _hwndSource;
        #endregion

        static FluentWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentWindow), new FrameworkPropertyMetadata(typeof(FluentWindow)));
        }

        public FluentWindow()
        {
            Loaded += FluentWindow_Loaded;
            Unloaded += FluentWindow_Unloaded;
        }

        public override void OnApplyTemplate()
        {
            _minButton = GetTemplateChild(MinButton) as Button;
            _maxButton = GetTemplateChild(MaxButton) as Button;
            _closeButton = GetTemplateChild(CloseButton) as Button;
        }

        private void FluentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _windowHandle = new(new WindowInteropHelper(this).EnsureHandle());

            _hwndSource = HwndSource.FromHwnd(_windowHandle);
            _hwndSource.AddHook(WindowProc);
        }

        private void FluentWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            _hwndSource.RemoveHook(WindowProc);
            _hwndSource.Dispose();
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wparam, nint lparam, ref bool handled)
        {
            if (msg == WM_NCHITTEST)
            {
                return new IntPtr(HitTestMouseOver(lparam, ref handled));
            }
            else if (msg == WM_NCLBUTTONDOWN)
            {
                return new IntPtr(HitTestMouseCaptured(lparam, ref handled));
            }
            else if (msg == WM_NCLBUTTONUP)
            {
                return new IntPtr(HitTestMousePressed(lparam, ref handled));
            }

            return IntPtr.Zero;
        }

        private uint HitTestMouseOver(nint lparam, ref bool handled)
        {
            _minButton.Tag = null;
            _maxButton.Tag = null;
            _closeButton.Tag = null;

            if (HitTestButton(lparam, out uint ht) is Button button)
            {
                handled = true;

                button.Tag = MouseOver;
            }

            return ht;
        }

        private uint HitTestMouseCaptured(nint lparam, ref bool handled)
        {
            _minButton.Tag = null;
            _maxButton.Tag = null;
            _closeButton.Tag = null;

            if (HitTestButton(lparam, out _) is Button button)
            {
                handled = true;
                button.Tag = MouseCaptured;
            }
            return 0;
        }

        private uint HitTestMousePressed(nint lparam, ref bool handled)
        {
            _minButton.Tag = null;
            _maxButton.Tag = null;
            _closeButton.Tag = null;

            if (HitTestButton(lparam, out _) is Button button)
            {
                handled = true;

                if (button == _minButton)
                {
                    WindowState = WindowState.Minimized;
                }
                else if (button == _maxButton)
                {
                    WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                }
                else
                {
                    Close();
                }
            }

            return 0;
        }

        private Button HitTestButton(nint lparam, out uint ht)
        {
            Point mousePoint = GetPoint(lparam);

            // Min
            {
                Point point = _minButton.PointToScreen(new Point(0, 0));

                Rect rect = new(point.X, point.Y, point.X + _minButton.ActualWidth, point.Y + _minButton.ActualHeight);

                if (mousePoint.X >= rect.X && mousePoint.X <= rect.Width && mousePoint.Y >= rect.Y && mousePoint.Y <= rect.Height)
                {
                    ht = HT_MINBUTTON;

                    return _minButton;
                }
            }

            // Max
            {
                Point point = _maxButton.PointToScreen(new Point(0, 0));

                Rect rect = new(point.X, point.Y, point.X + _maxButton.ActualWidth, point.Y + _maxButton.ActualHeight);

                if (mousePoint.X >= rect.X && mousePoint.X <= rect.Width && mousePoint.Y >= rect.Y && mousePoint.Y <= rect.Height)
                {
                    ht = HT_MAXBUTTON;

                    return _maxButton;
                }
            }

            // Close
            {
                Point point = _closeButton.PointToScreen(new Point(0, 0));

                Rect rect = new(point.X, point.Y, point.X + _closeButton.ActualWidth, point.Y + _closeButton.ActualHeight);

                if (mousePoint.X >= rect.X && mousePoint.X <= rect.Width && mousePoint.Y >= rect.Y && mousePoint.Y <= rect.Height)
                {
                    ht = HT_CLOSE;

                    return _closeButton;
                }
            }

            ht = HT_CLIENT;

            return null;
        }

        private static Point GetPoint(nint ptr)
        {
            uint xy = unchecked(Environment.Is64BitProcess ? (uint)ptr.ToInt64() : (uint)ptr.ToInt32());
            short x = unchecked((short)xy);
            short y = unchecked((short)(xy >> 16));
            return new Point(x, y);
        }
    }
}
