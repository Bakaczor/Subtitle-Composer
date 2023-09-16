using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;

namespace SubtitleComposer {
    /// <summary>
    /// Commands for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private void CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void OpenText_Executed(object sender, ExecutedRoutedEventArgs e) {
            LoadGridElements(e.Parameter, false);
        }

        private void OpenTranslation_Executed(object sender, ExecutedRoutedEventArgs e) {
            LoadGridElements(e.Parameter, true);
        }

        private void SaveText_Executed(object sender, ExecutedRoutedEventArgs e) {
            SaveGridElements(e.Parameter, false);
        }

        private void SaveTranslation_Executed(object sender, ExecutedRoutedEventArgs e) {
            SaveGridElements(e.Parameter, true);
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void About_Executed(object sender, ExecutedRoutedEventArgs e) {
            var about = new About();
            about.ShowDialog();
        }

        private void Translation_Executed(object sender, ExecutedRoutedEventArgs e) {
            TranslationItem.IsChecked = !TranslationItem.IsChecked;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e) {
            var openFileDialog = new OpenFileDialog {
                Filter = "All Media Files|*.wmv;*.avi;*.mpg;*.mpeg;*.mp4;*.mov;*.avi;*mkv|All files|*.*"
            };
            if (openFileDialog.ShowDialog() == true) {
                Player.Source = new Uri(openFileDialog.FileName);
            }
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = (Player != null) && (Player.Source != null) && !_isPlaying;
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e) {
            Player.Play();
            _isPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = _isPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e) {
            Player.Pause();
            _isPlaying = false;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e) {
            Player.Stop();
            _isPlaying = false;
        }
    }
}
