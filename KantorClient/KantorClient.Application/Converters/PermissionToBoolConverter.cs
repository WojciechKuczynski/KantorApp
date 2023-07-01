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
            if (value is UserPermission v1 && parameter is UserPermission v2)
            {
                return v1 >= v2;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
