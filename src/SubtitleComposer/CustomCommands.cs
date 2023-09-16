using System.Windows.Input;

namespace SubtitleComposer {
    public static class CustomCommands {
        public static readonly RoutedUICommand Open = new("Open", "Open", typeof(CustomCommands), new InputGestureCollection()
        {
            new KeyGesture(Key.O, ModifierKeys.Control)
        });

        public static readonly RoutedUICommand Exit = new("Exit", "Exit", typeof(CustomCommands), new InputGestureCollection()
        {
            new KeyGesture(Key.E, ModifierKeys.Control)
        });

        public static readonly RoutedUICommand Play = new("Play", "Play", typeof(CustomCommands), null);

        public static readonly RoutedUICommand Pause = new("Pause", "Pause", typeof(CustomCommands), null);

        public static readonly RoutedUICommand Stop = new("Stop", "Stop", typeof(CustomCommands), null);

        public static readonly RoutedUICommand OpenText = new("OpenText", "OpenText", typeof(CustomCommands), null);

        public static readonly RoutedUICommand OpenTranslation = new("OpenTranslation", "OpenTranslation", typeof(CustomCommands), null);

        public static readonly RoutedUICommand SaveText = new("SaveText", "SaveText", typeof(CustomCommands), null);

        public static readonly RoutedUICommand SaveTranslation = new("SaveTranslation", "SaveTranslation", typeof(CustomCommands), null);

        public static readonly RoutedUICommand About = new("About", "About", typeof(CustomCommands), new InputGestureCollection()
        {
            new KeyGesture(Key.H, ModifierKeys.Control)
        });

        public static readonly RoutedUICommand Translation = new("Translation", "Translation", typeof(CustomCommands), new InputGestureCollection()
        {
            new KeyGesture(Key.T, ModifierKeys.Control)
        });
    }
}
