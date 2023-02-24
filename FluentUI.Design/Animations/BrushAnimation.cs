using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FluentUI.Design.Animations;

public class BrushAnimation : AnimationTimeline
{
    #region Property
    public override Type TargetPropertyType => typeof(Brush);
    #endregion

    #region DependencyProperty
    public static readonly DependencyProperty FromProperty;
    public static readonly DependencyProperty ToProperty;
    public static readonly DependencyProperty EasingFunctionProperty;

    public Brush From
    {
        get { return (Brush)GetValue(FromProperty); }
        set { SetValue(FromProperty, value); }
    }

    public Brush To
    {
        get { return (Brush)GetValue(ToProperty); }
        set { SetValue(ToProperty, value); }
    }

    public IEasingFunction EasingFunction
    {
        get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
        set { SetValue(EasingFunctionProperty, value); }
    }
    #endregion

    static BrushAnimation()
    {
        FromProperty = DependencyProperty.Register(nameof(From), typeof(Brush), typeof(BrushAnimation), new PropertyMetadata(null));
        ToProperty = DependencyProperty.Register(nameof(To), typeof(Brush), typeof(BrushAnimation), new PropertyMetadata(null));
        EasingFunctionProperty = DependencyProperty.Register(nameof(EasingFunction), typeof(IEasingFunction), typeof(BrushAnimation), new PropertyMetadata(null));
    }

    protected override Freezable CreateInstanceCore()
    {
        return new BrushAnimation();
    }

    public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
    {
        if (!animationClock.CurrentProgress.HasValue)
        {
            return Brushes.Transparent;
        }

        Brush originValue = From ?? (Brush)defaultOriginValue;
        Brush dstValue = To ?? (Brush)defaultDestinationValue;

        double progress = animationClock.CurrentProgress.Value;
        if (progress == 0)
        {
            return originValue;
        }
        if (progress == 1)
        {
            return dstValue;
        }

        IEasingFunction easingFunction = this.EasingFunction;
        if (easingFunction != null)
        {
            progress = easingFunction.Ease(progress);
        }

        return new VisualBrush(new Border()
        {
            Width = 1,
            Height = 1,
            Background = originValue,
            Child = new Border()
            {
                Background = dstValue,
                Opacity = progress,
            }
        });
    }
}
