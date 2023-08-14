using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SubtitleComposer.Converters {
    public class TimeSpanToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is TimeSpan timeSpan) {
                string timeString;
                if (timeSpan < TimeSpan.FromSeconds(1)) {
                    timeString = timeSpan.ToString(@"s\.fff");
                } else {
                    timeString = timeSpan.ToString(@"hh\:mm\:ss\.fff").TrimStart(':', '0');
                }
                timeString = timeString.TrimEnd('0');
                timeString = timeString.TrimEnd('.');
                return timeString;
            }
            throw new ArgumentException("The parameter 'value' should be of type 'TimeSpan'.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string timeString) {
                if (timeString == "0") { return TimeSpan.Zero; }
                string[] formats = new[]
                {
                    @"s\.f", @"ss\.f", @"s\.ff", @"ss\.ff", @"s\.fff", @"ss\.fff",
                    @"m\:ss", @"mm\:ss", @"m\:ss\.f", @"mm\:ss\.f", @"m\:ss\.ff", @"mm\:ss\.ff", @"m\:ss\.fff", @"mm\:ss\.fff",
                    @"h\:mm\:ss", @"h\:mm\:ss\.f", @"h\:mm\:ss\.ff", @"h\:mm\:ss\.fff"
                };
                if (TimeSpan.TryParseExact(timeString, formats, culture, out TimeSpan timeSpan)) {
                    return timeSpan;
                } else {
                    return DependencyProperty.UnsetValue;
                }
            }
            throw new ArgumentException("The parameter 'value' should be of type 'string'.");
        }
    }
}
