using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KantorClient.Application.Converters
{
    public class BoolVisibilityInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility hideWay = Visibility.Collapsed;

            if (parameter is Visibility)
            {
                hideWay = (Visibility)parameter;
            }

            bool? val = value as bool?;
            return val == false ? Visibility.Visible : hideWay;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Collapsed.Equals(value);
        }
    }
}
