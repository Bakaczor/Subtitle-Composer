using System;
using System.Globalization;
using System.Windows.Data;

namespace SubtitleComposer.Converters {
    public class HeaderConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string text && parameter is string type) {
                int n = text.Length;
                if (type == "text") {
                    return $"Text: {n} characters";
                } else {
                    return $"Translation: {n} characters";
                }
            }
            throw new ArgumentException("The parameters 'value' and 'parameter' should be of type 'string'.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
