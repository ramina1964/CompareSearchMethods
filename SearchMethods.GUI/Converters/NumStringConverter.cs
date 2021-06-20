using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SearchMethods.Model.Properties;

namespace SearchMethods.GUI.Converters
{
    public class NumStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            const double limitAbsLog = 3;
            bool isAbsLogSmall = value is double db && Math.Abs(Math.Log10(db)) < limitAbsLog;

            if (value == null)
            { return null; }

            Type type = value.GetType();
            if (type == typeof(double))
            {
                double val = (double)value;
                return isAbsLogSmall
                    ? val.ToString("G3", CultureInfo.InvariantCulture)
                    : val.ToString("0.00E+00", CultureInfo.InvariantCulture);
            }

            if (type == typeof(long))
            {
                long val = (long)value;
                return isAbsLogSmall
                    ? val.ToString("G3", CultureInfo.InvariantCulture)
                    : val.ToString("0E+00", CultureInfo.InvariantCulture);
            }

            int intValue = (int)value;
            return isAbsLogSmall ?
                intValue.ToString("D3", CultureInfo.InvariantCulture) :
                intValue.ToString("G3", CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            if (!double.TryParse(str, out double result))
            {
                _ = MessageBox.Show(Resources.InvalidIntegerError);
            }

            return result;
        }
    }
}
