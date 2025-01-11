using System.Globalization;
using System.Windows.Data;

namespace Plottist.Resources.Converters
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * double.Parse(parameter.ToString()!, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / double.Parse(parameter.ToString()!, NumberStyles.Any, CultureInfo.InvariantCulture);
        }
    }
}
