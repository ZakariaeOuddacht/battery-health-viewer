using System;
using System.Collections.ObjectModel;
using System.Management;
using Battery_Health_Viewer.Views;
using Windows.Devices.Power;

namespace Battery_Health_Viewer.Services
{
    public class BatteryInfoProvider
    {
        // Provides info from the battery for the Dashboard (Menu.xaml) to use
        public ObservableCollection<BatteryProperty> GetBatteryInfo()
        {
            var battery = Battery.AggregateBattery;
            var report = battery.GetReport();

            long designCapacity = report.DesignCapacityInMilliwattHours ?? 0;
            long fullChargeCapacity = report.FullChargeCapacityInMilliwattHours ?? 0;
            long remainingCapacity = report.RemainingCapacityInMilliwattHours ?? 0;
            long chargeRate = report.ChargeRateInMilliwatts ?? 0;
            long voltage = 0;

            double currentPercent =
                fullChargeCapacity > 0
                    ? (double)remainingCapacity / fullChargeCapacity * 100
                    : 0;

            double health =
                designCapacity > 0
                    ? (double)fullChargeCapacity / designCapacity * 100
                    : 0;

            double wearLevel = 100 - health;

            string batteryName = "Unknown";
            string cycleCount = "Unknown";

            try
            {
                using var searcher = new ManagementObjectSearcher(
                    @"root\WMI",
                    "SELECT * FROM BatteryStaticData");

                foreach (ManagementObject batteryObj in searcher.Get())
                {
                    batteryName =
                        batteryObj["DeviceName"]?.ToString() ?? batteryName;
                    break;
                }
            }
            catch
            {

            }

            try
            {
                using var searcher =
                    new ManagementObjectSearcher("SELECT * FROM BatteryCycleCount");

                foreach (ManagementObject obj in searcher.Get())
                {
                    cycleCount =
                        obj["CycleCount"]?.ToString() ?? cycleCount;

                    break;
                }
            }
            catch
            {
                // Not supported on all systems
            }

            string powerState;

            if (chargeRate > 0)
                powerState = "Charging";
            else if (chargeRate < 0)
                powerState = "Discharging";
            else
                powerState = "AC Power";

            try
            {
                using var searcher = new ManagementObjectSearcher(
                    @"root\WMI",
                    "SELECT * FROM BatteryStatus");

                foreach (ManagementObject obj in searcher.Get())
                {
                    if (obj["Voltage"] != null)
                        voltage = Convert.ToInt64(obj["Voltage"]);

                    break;
                }
            }
            catch
            {
            }

            string FormatEnergy(long mwh)
            {
                if (AppSettings.UsemAh)
                {
                    double mah = mwh / 7.630789736754415; // I know, I've gone crazy here, but ATLEAST IT WORKS! :insane:
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
                new() { Description = "Charge/Discharge Rate", Value = $"{chargeRate:N0} mW" },

                new() { Description = "Charge Cycle Number", Value = cycleCount == "Unknown" ? "Not Supported Yet" : cycleCount },
            };
        }
    }
}