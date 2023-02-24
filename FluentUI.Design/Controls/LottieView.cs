using SkiaSharp;
using SkiaSharp.Skottie;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FluentUI.Design.Controls
{
    public class LottieView : Control
    {
        #region Constant
        private const string DrawCanvas = "DrawCanvas";
        #endregion

        #region Variable
        private Timer _timer;
        private SKElement _drawCanvas;
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty FilePathProperty;
        public static readonly DependencyProperty AnimationProperty;
        public static readonly DependencyProperty AutoSizeProperty;

        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        public Animation Animation
        {
            get { return (Animation)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        public bool AutoSize
        {
            get { return (bool)GetValue(AutoSizeProperty); }
            set { SetValue(AutoSizeProperty, value); }
        }
        #endregion

        static LottieView()
        {
            FilePathProperty = DependencyProperty.Register(nameof(FilePath), typeof(string), typeof(LottieView), new PropertyMetadata(string.Empty, OnFilePathChanged));
            AnimationProperty = DependencyProperty.Register(nameof(Animation), typeof(Animation), typeof(LottieView), new PropertyMetadata(null));
            AutoSizeProperty = DependencyProperty.Register(nameof(AutoSize), typeof(bool), typeof(LottieView), new PropertyMetadata(false));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(LottieView), new FrameworkPropertyMetadata(typeof(LottieView)));
        }

        public override void OnApplyTemplate()
        {
            _drawCanvas = GetTemplateChild(DrawCanvas) as SKElement;

            _drawCanvas.PaintSurface += DrawCanvas_PaintSurface;
        }

        private void DrawCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (Animation != null)
            {
                SKCanvas canvas = e.Surface.Canvas;
                canvas.Clear();

                Animation.Render(canvas, new SKRect(0, 0, e.Info.Width, e.Info.Height));
            }
        }

        private static void OnFilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LottieView lottieView && File.Exists(lottieView.FilePath))
            {
                lottieView.LoadAnimation();
            }
        }

        private void LoadAnimation()
        {
            _timer?.Stop();
            _timer?.Dispose();

            if (Animation.TryCreate(new MemoryStream(File.ReadAllBytes(FilePath)), out Animation animation))
            {
                Animation = animation;

                if (AutoSize)
                {
                    Width = Animation.Size.Width;
                    Height = Animation.Size.Height;
                }

                double seconds = 0;
                _timer = new Timer(1000d / Animation.Fps);
                _timer.Elapsed += (a, b) =>
                {
                    try
                    {
                        Dispatcher.Invoke(() =>
                        {
                            if (seconds > Animation.Duration.TotalSeconds)
                            {
                                seconds = 0;
                            }
                            Animation.SeekFrameTime(seconds);
                            _drawCanvas?.InvalidateVisual();
                        });
                    }
                    catch (TaskCanceledException)
                    {

                    }
                    seconds += _timer.Interval / 1000d;
                };
                _timer.Start();
            }
        }
    }
}
