using System;
using System.Globalization;
using System.Windows.Data;

namespace SubtitleComposer.Converters {
    public class TimeSpanToDoubleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is TimeSpan timeSpan) {
                return timeSpan.TotalSeconds;
            }
            throw new ArgumentException("The parameter 'value' should be of type 'double'.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
