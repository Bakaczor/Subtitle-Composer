using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SubtitleComposer {
    /// <summary>
    /// Events for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private void MenuItem_Add_Click(object sender, RoutedEventArgs e) {
            GridElements.Add(new GridElement());
        }

        private void MenuItem_AddAfter_Click(object sender, RoutedEventArgs e) {
            try {
                var selected = MainDataGrid.SelectedItems.Cast<GridElement>().ToArray();
                TimeSpan maxHideTime = selected.Max(idx => idx.Hide);
                int idx = GridElements.IndexOf(selected.Last());
                GridElements.Insert(idx + 1, new GridElement(maxHideTime, maxHideTime, "", ""));
            } catch {
                //no rows were selected
                GridElements.Add(new GridElement());
            }
        }

        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e) {
            try {
                var selected = MainDataGrid.SelectedItems.Cast<GridElement>().ToArray();
                foreach (GridElement item in selected) {
                    GridElements.Remove(item);
                }
            } catch {
                //no rows were selected
            }
        }

        private void SetText() {
            TimeSpan currentTime = Player.Position;
            var captions = GridElements.Where(el => currentTime >= el.Show && currentTime <= el.Hide).ToList();
            if (captions.Count == 0) {
                Captions.Text = null;
                Captions.Visibility = Visibility.Collapsed;
                return;
            }
            Captions.Text = string.Join("\n", captions.Select(el => el.Text));
            Captions.Visibility = Visibility.Visible;
        }

        private void SetTranslation() {
            TimeSpan currentTime = Player.Position;
            var captions = GridElements.Where(el => currentTime >= el.Show && currentTime <= el.Hide).ToList();
            if (captions.Count == 0) {
                Captions.Text = null;
                Captions.Visibility = Visibility.Collapsed;
                return;
            }
            Captions.Text = string.Join("\n", captions.Select(el => el.Translation));
            Captions.Visibility = Visibility.Visible;
        }

        private void Timer_Tick(object? sender, EventArgs e) {
            if (Player.Source != null && Player.NaturalDuration.HasTimeSpan && !_isDragging) {
                MainSlider.Minimum = 0;
                MainSlider.Maximum = Player.NaturalDuration.TimeSpan.TotalSeconds;
                MainSlider.Value = Player.Position.TotalSeconds;
            }
            if (TranslationItem.IsChecked) {
                SetTranslation();
            } else {
                SetText();
            }
        }

        private void MainSlider_DragStarted(object sender, DragStartedEventArgs e) {
            _isDragging = true;
        }

        private void MainSlider_DragCompleted(object sender, DragCompletedEventArgs e) {
            _isDragging = false;
            Player.Position = TimeSpan.FromSeconds(MainSlider.Value);
        }

        private void Volume_MouseWheel(object sender, MouseWheelEventArgs e) {
            Player.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void PlayPause_MouseClick(object sender, MouseButtonEventArgs e) {
            if (_isPlaying) {
                Player.Pause();
                _isPlaying = false;
            } else {
                Player.Play();
                _isPlaying = true;
            }
        }

        private void MainDataGrid_Select(object sender, SelectionChangedEventArgs e) {
            if (sender is DataGrid grid && grid.SelectedItem is GridElement selected) {
                Player.Position = selected.Show;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Space) {
                if (CustomCommands.Play.CanExecute(null, null)) {
                    CustomCommands.Play.Execute(null, null);
                } else if (CustomCommands.Pause.CanExecute(null, null)) {
                    CustomCommands.Pause.Execute(null, null);
                }
            }
        }

    }
}
