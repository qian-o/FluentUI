using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace FluentUI.Design.Animations;

[DependencyProperty<Point>("Point1")]
[DependencyProperty<Point>("Point2")]
public partial class BezierEase : EasingFunctionBase
{
    private readonly Point[] _controlPoints =
    [
        new Point(0, 0),
        new Point(0.5, 0.5),
        new Point(0.5, 0.5),
        new Point(1, 1)
    ];

    partial void OnPoint1Changed(Point oldValue, Point newValue)
    {
        _controlPoints[1] = Point1;
    }

    partial void OnPoint2Changed(Point oldValue, Point newValue)
    {
        _controlPoints[2] = Point2;
    }

    protected override Freezable CreateInstanceCore()
    {
        return new BezierEase();
    }

    protected override double EaseInCore(double normalizedTime)
    {
        return GetPointIter(_controlPoints, normalizedTime).Y;
    }

    private static Point GetPointIter(ICollection<Point> points, double posX)
    {
        if (points.Count == 1)
        {
            return points.ElementAt(0);
        }

        var result = new List<Point>();

        for (var i = 1; i < points.Count; i++)
        {
            var point1 = points.ElementAt(i - 1);
            var point2 = points.ElementAt(i);
            result.Add(GetPointOnLine(point1, point2, posX));
        }

        return GetPointIter(result, posX);
    }

    private static Point GetPointOnLine(Point point1, Point point2, double posX)
    {
        var x1 = point1.X;
        var x2 = point2.X;
        var y1 = point1.Y;
        var y2 = point2.Y;

        var x = x1 + ((x2 - x1) * posX);
        var y = y1 + ((y2 - y1) * posX);

        return new Point(x, y);
    }
}
