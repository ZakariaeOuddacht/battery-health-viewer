using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Battery_Health_Viewer.Services;

namespace Battery_Health_Viewer.Views
{
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();

            LoadSettings();
        }

        private void LoadSettings()
        {
            // Theme
            ThemeCombo.SelectedIndex = AppSettings.Theme switch
            {
                "Light" => 1,
                "Dark" => 2,
                _ => 0
            };

            // Auto-refresh speed
            RefreshCombo.SelectedIndex = AppSettings.RefreshSpeed switch
            {
                5000 => 0,
                2000 => 1,
                1000 => 2,
                _ => 1
            };

            ThemeCombo.SelectionChanged += ThemeChanged;
            RefreshCombo.SelectionChanged += RefreshChanged;
        }

        private void ThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            string theme = ThemeCombo.SelectedIndex switch
            {
                1 => "Light",
                2 => "Dark",
                _ => "System"
            };

            AppSettings.Theme = theme;

            var mainWindow = (MainWindow)((App)Application.Current).MainWindow;
            mainWindow.SetTheme(theme);
        }
        private void RefreshChanged(object sender, SelectionChangedEventArgs e)
        {
            int value = RefreshCombo.SelectedIndex switch
            {
                0 => 5000,
                1 => 2000,
                2 => 1000,
                _ => 2000
            };

            AppSettings.RefreshSpeed = value;

            var mainWindow = (MainWindow)((App)Application.Current).MainWindow;

            var menu = mainWindow.RootFrame.Content as Menu;
            menu?.UpdateRefreshSpeed();
        }
    }
}