namespace Battery_Health_Viewer.Services
{
    public class BatteryReportInfo
    {
        public string Manufacturer { get; set; } = "Unknown";
        public string SerialNumber { get; set; } = "Unknown";
        public string ManufactureDate { get; set; } = "Unknown";
        public string CycleCount { get; set; } = "Unknown";
    }
}