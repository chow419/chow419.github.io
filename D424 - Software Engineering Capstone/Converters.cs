using System.Globalization;

namespace D424___Software_Engineering_Capstone.Converters
{
    class Converters
    {
    }
    public class BoolToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? "Yes" : "No";
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
