using System;
using System.Globalization;
using System.Windows.Data;

namespace VacationHelper
{
    internal class DayStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string) && value is DateTime)
            {
                return ((DateTime)value).ToString("d");
            }

            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null
                ? DateTime.Parse(value.ToString())
                : DateTime.MinValue;
        }
    }
}
