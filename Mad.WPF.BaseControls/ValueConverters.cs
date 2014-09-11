using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Mad.WPF.BaseControls
{
    [ValueConversion(typeof(double), typeof(CornerRadius))]
    public class UCCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new CornerRadius((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((CornerRadius)value).TopLeft;
        }
    }


    [ValueConversion(typeof(string), typeof(Brush))]
    public class UCBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                BrushConverter brushConverter = new BrushConverter();
                Brush brush = (Brush)brushConverter.ConvertFromString(value.ToString());
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Color color = ((SolidColorBrush)value).Color;
                return color.ToString();
            }
            return null;
        }
    }


    [ValueConversion(typeof(string), typeof(Color))]
    public class UCColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return (Color)ColorConverter.ConvertFromString(value.ToString());
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return ((Color)value).ToString();
            }
            return null;
        }
    }
}
