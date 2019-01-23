using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SimpleDialogs.ValueConverters
{
    public class PercentageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && double.TryParse(values[0].ToString(), out double value) && double.TryParse(values[1].ToString(), out double percentage))
            {
                return (percentage / 100d) * value;
            }

            return double.NaN;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
