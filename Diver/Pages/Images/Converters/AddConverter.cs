using System;
using System.Globalization;
using System.Windows.Data;

namespace Diver.Pages.Images.Converters
{
    [ValueConversion(sourceType: typeof(double), targetType: typeof(double))]
    internal class AddConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = double.Parse(value.ToString()) + double.Parse(parameter.ToString());

            return Math.Max(result, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
