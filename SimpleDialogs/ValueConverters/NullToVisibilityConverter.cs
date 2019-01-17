using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimpleDialogs.ValueConverters
{
    class NullToVisibilityConverter : IValueConverter
    {
        public NullToVisibilityConverter()
        {
            TrueValue = Visibility.Hidden;
            FalseValue = Visibility.Visible;
        }

        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
