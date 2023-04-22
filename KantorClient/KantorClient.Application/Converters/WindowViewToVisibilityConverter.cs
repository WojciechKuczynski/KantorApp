using KantorClient.Application.Consts;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KantorClient.Application.Converters
{
    public class WindowViewToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;
            try
            {
                var view = (MainWindowView)value;
                var intpar = System.Convert.ToInt32(parameter);
                MainWindowView par = (MainWindowView)Enum.ToObject(typeof(MainWindowView), intpar);

                return view == par ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
