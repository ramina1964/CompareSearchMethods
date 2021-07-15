using System;
using System.Globalization;
using System.Windows.Data;

namespace SearchMethods.GUI.Converters
{
    public class NegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isConverted = bool.TryParse(value.ToString(), out bool result);
            return isConverted
                   ? (object)!result
                   : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isConverted = bool.TryParse(value.ToString(), out bool result);
            return isConverted
                   ? (object)!result
                   : null;
        }
    }
}
