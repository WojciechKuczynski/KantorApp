using KantorClient.BLL.Models;
using KantorClient.Model.Consts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KantorClient.Application.Converters
{
    public class DefaultRateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is RateModel rate && values[1] is TransactionType transType)
            {
                return (transType == TransactionType.Buy ? rate.DefaultBuyRate : rate.DefaultSellRate).ToString();
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
