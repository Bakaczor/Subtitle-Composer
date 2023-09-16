using System;
using System.ComponentModel;

namespace SubtitleComposer {
    public class GridElement : INotifyPropertyChanged, IDataErrorInfo {
        private static TimeSpan _maxHideTime;

        private TimeSpan vShow;

        public TimeSpan Show {
            get => vShow;
            set {
                if (vShow != value) {
                    vShow = value;
                    OnPropertyChanged(nameof(Show));
                    Duration = Hide - Show;
                }
            }
        }

        private TimeSpan vHide;

        public TimeSpan Hide {
            get => vHide;
            set {
                if (vHide != value) {
                    vHide = value;
                    OnPropertyChanged(nameof(Hide));
                    Duration = Hide - Show;
                    if (Hide > _maxHideTime) {
                        _maxHideTime = Hide;
                    }
                }
            }
        }

        private TimeSpan vDuration;

        public TimeSpan Duration {
            get => vDuration;
            set {
                if (vDuration != value) {
                    vDuration = value;
                    OnPropertyChanged(nameof(Duration));
                    Hide = Show + Duration;
                    if (Hide > _maxHideTime) {
                        _maxHideTime = Hide;
                    }
                }
            }
        }

        private string vText;

        public string Text {
            get => vText;
            set {
                if (vText != value) {
                    vText = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        private string vTranslation;

        public string Translation {
            get => vTranslation;
            set {
                if (vTranslation != value) {
                    vTranslation = value;
                    OnPropertyChanged(nameof(Translation));
                }
            }
        }

        public GridElement() {
            vShow = _maxHideTime;
            vHide = _maxHideTime;
            vDuration = TimeSpan.Zero;
            vText = "";
            vTranslation = "";
            Error = "";
        }

        public GridElement(TimeSpan show, TimeSpan hide, string text, string translation) {
            vShow = show;
            vHide = hide;
            vDuration = hide - show;
            vText = text;
            vTranslation = translation;
            Error = "";
            if (Hide > _maxHideTime) {
                _maxHideTime = Hide;
            }

        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //IDataErrorInfo
        public string Error { get; }

        public string this[string columnName] {
            get {
                switch (columnName) {
                    case nameof(Show) or nameof(Hide): {
                            if (Show < TimeSpan.Zero) {
                                return "Show must be a positive value.";
                            }
                            if (Show > Hide) {
                                return "Show must be smaller tha Hide.";
                            }
                            break;
                        }
                    case nameof(Duration): {
                            if (Duration < TimeSpan.Zero) {
                                return "Duration must be a positive value.";
                            }
                            break;
                        }
                }
                return "";
            }
        }

    }
}
