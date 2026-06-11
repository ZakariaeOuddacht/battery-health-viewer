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
            CapacityToggle.IsOn = AppSettings.UsemAh;

            CapacityToggle.Toggled += (s, e) =>
            {
                AppSettings.UsemAh = CapacityToggle.IsOn;
            };

            CensorToggle.IsOn = AppSettings.CensorSerial;

            CensorToggle.Toggled += (s, e) =>
            {
                AppSettings.CensorSerial = CensorToggle.IsOn;
            };

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
                5 => 0,
                2 => 1,
                1 => 2,
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

            if (Application.Current is App app && app.MainWindow is MainWindow mainWindow)
            {
                mainWindow.SetTheme(theme);
            }
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

            if (Application.Current is App app && app.MainWindow is MainWindow mainWindow)
            {
                var menu = mainWindow.RootFrame.Content as Menu;
                menu?.UpdateRefreshSpeed();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.ResetToDefaults();

            CapacityToggle.IsOn = AppSettings.UsemAh;
            CensorToggle.IsOn = AppSettings.CensorSerial;

            ThemeCombo.SelectedIndex = 0;
            RefreshCombo.SelectedIndex = 1;

            if (Application.Current is App app && app.MainWindow is MainWindow mainWindow)
            {
                mainWindow.SetTheme(AppSettings.Theme);
            }

            if (Application.Current is App app2 && app2.MainWindow is MainWindow mainWindow2)
            {
                var menu = mainWindow2.RootFrame.Content as Menu;
                menu?.UpdateRefreshSpeed();
            }
        }
    }
}