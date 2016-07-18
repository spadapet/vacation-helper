using System;
using System.Globalization;
using System.Windows.Data;

namespace VacationHelper
{
    internal class TimeSpanToDaysConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double scale = parameter != null
                ? double.Parse(parameter.ToString())
                : 1.0;

            if (targetType == typeof(int))
            {
                if (!(value is TimeSpan))
                {
                    return 0;
                }

                TimeSpan span = (TimeSpan)value;
                return (int)(span.TotalDays / scale);
            }

            if (targetType == typeof(double))
            {
                if (!(value is TimeSpan))
                {
                    return 0.0;
                }

                TimeSpan span = (TimeSpan)value;
                return span.TotalDays / scale;
            }

            if (targetType == typeof(string))
            {
                if (!(value is TimeSpan))
                {
                    return string.Empty;
                }

                TimeSpan span = (TimeSpan)value;
                return ((int)(span.TotalDays / scale)).ToString(CultureInfo.InvariantCulture);
            }

            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double scale = parameter != null
                ? double.Parse(parameter.ToString())
                : 1.0;

            if (targetType != typeof(TimeSpan))
            {
                return null;
            }

            if (value is int)
            {
                return TimeSpan.FromDays((int)value * scale);
            }

            if (value is double)
            {
                return TimeSpan.FromDays((double)value * scale);
            }

            if (value is string)
            {
                double days;
                if (double.TryParse((string)value, out days))
                {
                    return TimeSpan.FromDays(days * scale);
                }
            }

            return null;
        }
    }
}
