using Battery_Health_Viewer.Views;
using System;
using System.Collections.ObjectModel;
using Windows.Devices.Power;

namespace Battery_Health_Viewer.Services
{
    public class BatteryInfoProvider
    {
        private readonly BatteryService _service = new();

        public ObservableCollection<BatteryProperty> GetBatteryInfo()
        {
            var report = _service.GetReport();

            long design = report.DesignCapacityInMilliwattHours ?? 0;
            long full = report.FullChargeCapacityInMilliwattHours ?? 0;
            long current = report.RemainingCapacityInMilliwattHours ?? 0;

            double health = design > 0 && full > 0
                ? (double)full / design * 100
                : 0;

            return new ObservableCollection<BatteryProperty>
            {
                new BatteryProperty { Description = "Design Capacity", Value = $"{design:N0} mWh" },
                new BatteryProperty { Description = "Full Charge Capacity", Value = $"{full:N0} mWh" },
                new BatteryProperty { Description = "Current Capacity", Value = $"{current:N0} mWh" },
                new BatteryProperty { Description = "Battery Health", Value = $"{health:F1}%" },
                new BatteryProperty { Description = "Charge Status", Value = report.Status.ToString() },
            };
        }
    }
}