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

    public class PhoneNumberFormatterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var phone = value as string;
            if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10)
                return phone;

            return $"({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Optional: Remove formatting if needed
            var formatted = value as string;
            if (string.IsNullOrWhiteSpace(formatted))
                return formatted;

            return new string(formatted.Where(char.IsDigit).ToArray());
        }
    }
}
