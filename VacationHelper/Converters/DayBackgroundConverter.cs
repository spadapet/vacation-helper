using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace VacationHelper
{
    internal class DayBackgroundConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
            {
                return null;
            }

            if (values == null || values.Length < 2)
            {
                return null;
            }

            if (!(values[0] is DateTime) || !(values[1] is VacationData))
            {
                return null;
            }

            DateTime dt = (DateTime)values[0];
            VacationData data = (VacationData)values[1];

            return data.GetBackgroundBrush(dt);
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
