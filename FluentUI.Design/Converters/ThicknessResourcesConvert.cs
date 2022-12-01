using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FluentUI.Design.Converters
{
    public class ThicknessResourcesConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new Thickness(System.Convert.ToDouble(values[0]), System.Convert.ToDouble(values[1]), System.Convert.ToDouble(values[2]), System.Convert.ToDouble(values[3]));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is Thickness thickness)
            {
                return new object[]
                {
                    thickness.Left,
                    thickness.Top,
                    thickness.Right,
                    thickness.Bottom
                };
            }
            return null;
        }
    }
}
