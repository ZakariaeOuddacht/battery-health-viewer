using Windows.Storage;

namespace Battery_Health_Viewer.Services
{
    public static class AppSettings
    {
        private static ApplicationDataContainer Local => ApplicationData.Current.LocalSettings;

        public static int RefreshSpeed
        {
            get => (int)(Local.Values["RefreshSpeed"] ?? 2000);
            set => Local.Values["RefreshSpeed"] = value;
        }

        public static string Theme
        {
            get => (string)(Local.Values["Theme"] ?? "System");
            set => Local.Values["Theme"] = value;
        }
    }
}