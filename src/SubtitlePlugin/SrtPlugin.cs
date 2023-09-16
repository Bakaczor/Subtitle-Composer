using System.Text;
using System.Text.RegularExpressions;


namespace SubtitlePlugin {
    public class SrtPlugin : ISubtitlePlugin {
        private readonly string _timePattern = @"(\d{2}:\d{2}:\d{2},\d{3})\s*-->\s*(\d{2}:\d{2}:\d{2},\d{3})";

        public string Name => "SubRip";

        public string Extension => ".srt";

        public override string ToString() {
            return Name;
        }

        public ICollection<SubtitleRecord> Load(string path) {
            if (!File.Exists(path)) {
                throw new FileNotFoundException("Failed to locate the file.");
            }
            var records = new List<SubtitleRecord>();

            using var reader = new StreamReader(File.OpenRead(path));
            string? line;
            string recordNumber, showTime, hideTime;
            var text = new StringBuilder();

            while ((line = reader.ReadLine()) != null) {
                if (line == "") {
                    //skip empty lines
                    bool flag = false;
                    while (string.IsNullOrEmpty(line = reader.ReadLine())) {
                        if (line == null) {
                            flag = true;
                            break;
                        }
                    }
                    if (flag) { break; }
                }
                //read line with number
                recordNumber = line!;
                //read line with timestamp
                if ((line = reader.ReadLine()) == null) {
                    throw new InvalidDataException("Input file is in wrong format.");
                }
                Match match = Regex.Match(line, _timePattern);
                if (match.Success) {
                    showTime = match.Groups[1].Value;
                    hideTime = match.Groups[2].Value;
                } else {
                    throw new InvalidDataException("Timestamps are in wrong format.");
                }
                //read lines with text
                while (!string.IsNullOrEmpty(line = reader.ReadLine())) {
                    text.AppendLine(line);
                }
                records.Add(new SubtitleRecord(recordNumber, showTime, hideTime, text.ToString().TrimEnd('\r', '\n')));
                text.Clear();
            }
            return records;
        }

        public void Save(string path, ICollection<SubtitleRecord> subtitles) {
            using var writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
            int i = 1;
            foreach (SubtitleRecord record in subtitles) {
                writer.WriteLine(i++.ToString());
                writer.WriteLine($"{record.ShowTime} --> {record.HideTime}");
                writer.WriteLine(record.Text + "\n");
            }
        }
    }
}
