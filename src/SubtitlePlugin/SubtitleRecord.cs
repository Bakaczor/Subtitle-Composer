using System.Globalization;

namespace SubtitlePlugin {
    public class SubtitleRecord {
        public int RecordNumber { get; }

        public TimeSpan ShowTime { get; }

        public TimeSpan HideTime { get; }

        public string Text { get; }

        public SubtitleRecord(int recordNumber, TimeSpan showTime, TimeSpan hideTime, string text) {
            RecordNumber = recordNumber;
            ShowTime = showTime;
            HideTime = hideTime;
            Text = text;
        }

        public SubtitleRecord(string recordNumber, string showTime, string hideTime, string text) {
            RecordNumber = int.Parse(recordNumber);
            ShowTime = TimeSpan.ParseExact(showTime, @"hh\:mm\:ss\,fff", CultureInfo.CurrentCulture);
            HideTime = TimeSpan.ParseExact(hideTime, @"hh\:mm\:ss\,fff", CultureInfo.CurrentCulture);
            Text = text;
        }
    }
}