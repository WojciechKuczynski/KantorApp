using KantorClient.BLL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace KantorClient.Application.Converters
{
    public class ReportModelToSumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<TransactionReportModel> models)
            {
                if (parameter.ToString() == "1")
                {
                    return models.Sum(x => x.FinalValue);
                }
                else if (parameter.ToString() == "2")
                {
                    return models.Sum(x => x.Quantity);
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
