using Battery_Health_Viewer.Views;
using System;
using System.Collections.ObjectModel;
using System.Management;
using System.Threading.Tasks;
using Windows.Devices.Power;

namespace Battery_Health_Viewer.Services
{
    public class BatteryInfoProvider
    {
        // Provides info from the battery for the Dashboard (Menu.xaml) to use

        // Info that is extracted from XML
        public async Task<ObservableCollection<BatteryProperty>> GetBatteryInfoAsync()
        {
            var reportService = new BatteryReportService();
            var reporte = await reportService.LoadAsync();

            return new ObservableCollection<BatteryProperty>
            {
                new() { Description = "Manufacturer", Value = reporte.Manufacturer },
                new()
                {
                    Description = "Serial Number",
                    Value = AppSettings.CensorSerial ? Censor(reporte.SerialNumber) : reporte.SerialNumber
                },
                new() { Description = "Manufacture Date", Value = reporte.ManufactureDate },
                new() { Description = "Charge Cycle Number", Value = reporte.CycleCount }
            };
        }

        // The Updated One
        public ObservableCollection<BatteryProperty> GetLiveBatteryInfo()
        {
            try { System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce; }
            catch { }

            var battery = Battery.AggregateBattery;
            var report = battery.GetReport();

            long designCapacity = report.DesignCapacityInMilliwattHours ?? 0;
            long fullChargeCapacity = report.FullChargeCapacityInMilliwattHours ?? 0;
            long remainingCapacity = report.RemainingCapacityInMilliwattHours ?? 0;
            long chargeRate = report.ChargeRateInMilliwatts ?? 0;
            long voltage = 0;

            try
            {
                using var searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM BatteryStatus");
                foreach (ManagementObject obj in searcher.Get())
                {
                    if (obj["Voltage"] != null) voltage = Convert.ToInt64(obj["Voltage"]);
                    break;
                }
            }
            catch { voltage = 0; }

            string batteryName = "Unknown";
            try
            {
                using var searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM BatteryStaticData");
                foreach (ManagementObject batteryObj in searcher.Get())
                {
                    batteryName = batteryObj["DeviceName"]?.ToString() ?? "Unknown";
                    break;
                }
            }
            catch { }

            double currentPercent = fullChargeCapacity > 0 ? (double)remainingCapacity / fullChargeCapacity * 100 : 0;
            double health = designCapacity > 0 ? (double)fullChargeCapacity / designCapacity * 100 : 0;
            double wearLevel = 100 - health;

            string powerState = chargeRate > 0 ? "Charging" : chargeRate < 0 ? "Discharging" : "AC Power";

            string FormatEnergy(long mwh)
            {
                if (AppSettings.UsemAh)
                {
                    double mah = voltage > 0 ? mwh / (voltage / 1000.0) : mwh / 7.630789736754415;  // I know, I've gone crazy here, but ATLEAST IT WORKS! :insane:
                    return $"{mah:N0} mAh";
                }
                return $"{mwh:N0} mWh";
            }

            return new ObservableCollection<BatteryProperty>
            {
                new() { Description = "Battery Name", Value = batteryName },
                new() { Description = "Power State", Value = powerState },
                new() { Description = "Current Capacity (in %)", Value = $"{currentPercent:F1}%" },
                new() { Description = "Current Capacity Value", Value = FormatEnergy(remainingCapacity) },
                new() { Description = "Full Charge Capacity", Value = FormatEnergy(fullChargeCapacity) },
                new() { Description = "Designed Capacity", Value = FormatEnergy(designCapacity) },
                new() { Description = "Battery Health", Value = $"{health:F1}%" },
                new() { Description = "Wear Level", Value = $"{wearLevel:F1}%" },
                new() { Description = "Voltage", Value = $"{voltage:N0} mV" },
                new() { Description = "Charge/Discharge Rate", Value = $"{chargeRate:N0} mW" }
            };
        }

        private static string Censor(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return new string('*', input.Length);
        }
    }
}