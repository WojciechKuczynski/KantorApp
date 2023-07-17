using System;
using System.Globalization;
using System.Windows.Data;

namespace KantorClient.Application.Converters
{
    public class BoolStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool v && v ? parameter.ToString() : "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
