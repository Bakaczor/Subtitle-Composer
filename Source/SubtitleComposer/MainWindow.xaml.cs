using Microsoft.Win32;
using SubtitlePlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

///MIT License

//Copyright(c) 2023 Bartosz Kaczorowski

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

namespace SubtitleComposer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private bool _isPlaying = false;
        private bool _isDragging = false;
        private readonly DispatcherTimer timer;

        public ObservableCollection<GridElement> GridElements { get; set; }

        public List<ISubtitlePlugin> Plugins { get; set; }

        public MainWindow() {
            InitializeComponent();

            Plugins = new() { new SrtPlugin() };
            GridElements = new();
            DataContext = this;

            timer = new() {
                Interval = TimeSpan.FromMilliseconds(10)
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            KeyDown += MainWindow_KeyDown;
        }

        private void LoadGridElements(object parameter, bool translation) {
            var dialog = new OpenFileDialog {
                Filter = "All files|*.*|SubRip|*.srt"
            };
            if (dialog.ShowDialog() == true) {
                if (parameter is ISubtitlePlugin plugin) {
                    ICollection<SubtitleRecord> records = plugin.Load(dialog.FileName);
                    if (records.Count == GridElements.Count) {
                        int i = 0;
                        foreach (SubtitleRecord record in records) {
                            GridElements[i].Show = record.ShowTime;
                            GridElements[i].Hide = record.HideTime;
                            if (translation) {
                                GridElements[i].Translation = record.Text;
                            } else {
                                GridElements[i].Text = record.Text;
                            }
                            i++;
                        }
                    } else {
                        GridElements.Clear();
                        foreach (SubtitleRecord record in records) {
                            if (translation) {
                                GridElements.Add(new GridElement(record.ShowTime, record.HideTime, "", record.Text));
                            } else {
                                GridElements.Add(new GridElement(record.ShowTime, record.HideTime, record.Text, ""));
                            }
                        }
                    }
                } else {
                    throw new Exception("The argument should be of type 'ISubtitlePlugin'.");
                }
            }
        }

        private void SaveGridElements(object parameter, bool translation) {
            var dialog = new SaveFileDialog {
                AddExtension = false,
                Filter = "All files|*.*|SubRip|*.srt"
            };
            if (dialog.ShowDialog() == true) {
                if (parameter is ISubtitlePlugin plugin) {
                    var records = new List<SubtitleRecord>();
                    int i = 0;
                    foreach (GridElement element in GridElements) {
                        if (translation) {
                            records.Add(new SubtitleRecord(i, element.Show, element.Hide, element.Translation));
                        } else {
                            records.Add(new SubtitleRecord(i, element.Show, element.Hide, element.Text));
                        }
                        i++;
                    }
                    plugin.Save(dialog.FileName, records);
                } else {
                    throw new Exception("The argument should be of type 'ISubtitlePlugin'.");
                }
            }
        }
    }
}
