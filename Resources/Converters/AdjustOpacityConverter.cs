using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Plottist.Resources.Converters
{
    public class AdjustOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = value as Color?;
            if (color is null) return value;
            var opacity = double.Parse(parameter.ToString()!, NumberStyles.Any, CultureInfo.InvariantCulture);
            byte newAlpha = (byte)(opacity * 255);
            return Color.FromArgb(newAlpha, color.Value.R, color.Value.G, color.Value.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
