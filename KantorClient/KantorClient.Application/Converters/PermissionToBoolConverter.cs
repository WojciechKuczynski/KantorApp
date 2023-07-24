using KantorClient.Model.Consts;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KantorClient.Application.Converters
{
    public class PermissionToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string v1 && parameter is string v2)
            {
                return v1.Contains(v2);
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
