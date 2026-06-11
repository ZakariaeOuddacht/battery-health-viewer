using Windows.Storage;

namespace Battery_Health_Viewer.Services
{
    public static class AppSettings
    {
        // A class that handles the app's settings
        private static ApplicationDataContainer Local => ApplicationData.Current.LocalSettings;

        public static int RefreshSpeed
        {
            get => (int)(Local.Values["RefreshSpeed"] ?? 2);
            set => Local.Values["RefreshSpeed"] = value;
        }

        public static string Theme
        {
            get => (string)(Local.Values["Theme"] ?? "System");
            set => Local.Values["Theme"] = value;
        }

        public static bool UsemAh
        {
            get => (bool)(Local.Values["UsemAh"] ?? false);
            set => Local.Values["UsemAh"] = value;
        }

        public static bool CensorSerial // Or I'd say CENSOR CEREAL (ba dum tss)
        {
            get => (bool)(Local.Values["CensorSerial"] ?? false);
            set => Local.Values["CensorSerial"] = value;
        }
        // Window specific
        public static int WindowWidth
        {
            get => (int?)Local.Values["WindowWidth"] ?? 1280;
            set => Local.Values["WindowWidth"] = value;
        }

        public static int WindowHeight
        {
            get => (int?)Local.Values["WindowHeight"] ?? 800;
            set => Local.Values["WindowHeight"] = value;
        }

        public static int WindowX
        {
            get => (int?)Local.Values["WindowX"] ?? -1;
            set => Local.Values["WindowX"] = value;
        }

        public static int WindowY
        {
            get => (int?)Local.Values["WindowY"] ?? -1;
            set => Local.Values["WindowY"] = value;
        }

        public static bool WindowMaximized
        {
            get => (bool?)Local.Values["WindowMaximized"] ?? false;
            set => Local.Values["WindowMaximized"] = value;
        }
    }
}