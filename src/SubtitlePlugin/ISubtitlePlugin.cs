namespace SubtitlePlugin {
    public interface ISubtitlePlugin {
        string Name { get; }

        string Extension { get; }

        ICollection<SubtitleRecord> Load(string path);

        void Save(string path, ICollection<SubtitleRecord> subtitles);
    }
}
