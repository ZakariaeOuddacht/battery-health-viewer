using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Battery_Health_Viewer.Services;

namespace Battery_Health_Viewer.Views
{
    public sealed partial class Menu : Page
    {
        public ObservableCollection<BatteryProperty> BatteryInfo { get; } = new();

        private readonly BatteryInfoProvider _provider = new();
        private readonly DispatcherTimer _timer = new();

        public Menu()
        {
            InitializeComponent();

            BatteryInfo.Add(new BatteryProperty { Description = "Battery Name" });
            BatteryInfo.Add(new BatteryProperty { Description = "Power State" });
            BatteryInfo.Add(new BatteryProperty { Description = "Current Capacity (in %)" });
            BatteryInfo.Add(new BatteryProperty { Description = "Current Capacity Value" });
            BatteryInfo.Add(new BatteryProperty { Description = "Full Charge Capacity" });
            BatteryInfo.Add(new BatteryProperty { Description = "Designed Capacity" });
            BatteryInfo.Add(new BatteryProperty { Description = "Battery Health" });
            BatteryInfo.Add(new BatteryProperty { Description = "Wear Level" });
            BatteryInfo.Add(new BatteryProperty { Description = "Voltage" });
            BatteryInfo.Add(new BatteryProperty { Description = "Charge/Discharge Rate" });
            BatteryInfo.Add(new BatteryProperty { Description = "Charge Cycle Number" });

            RefreshBattery();

            _timer.Interval = TimeSpan.FromMilliseconds(AppSettings.RefreshSpeed);
            _timer.Tick += (_, __) => RefreshBattery();
            _timer.Start();
        }

        private void RefreshBattery()
        {
            var updated = _provider.GetBatteryInfo();

            int count = Math.Min(BatteryInfo.Count, updated.Count);

            for (int i = 0; i < count; i++)
            {
                BatteryInfo[i].Value = updated[i].Value;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshBattery();
        }
        public void UpdateRefreshSpeed()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(AppSettings.RefreshSpeed);
        }
    }
}