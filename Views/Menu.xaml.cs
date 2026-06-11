using Battery_Health_Viewer.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Power;

namespace Battery_Health_Viewer.Views
{
    public sealed partial class Menu : Page
    {
        public ObservableCollection<BatteryProperty> BatteryInfo { get; } = new();
        private readonly BatteryInfoProvider _provider = new();

        private readonly DispatcherTimer _fastTimer = new();
        private readonly DispatcherTimer _slowTimer = new();

        public Menu()
        {
            InitializeComponent();

            // Placeholders
            string[] descriptions = {
                "Battery Name", "Manufacturer", "Serial Number", "Manufacture Date",
                "Power State", "Current Capacity (in %)", "Current Capacity Value",
                "Full Charge Capacity", "Designed Capacity", "Battery Health",
                "Wear Level", "Voltage", "Charge/Discharge Rate", "Charge Cycle Number"
            };

            foreach (var desc in descriptions)
            {
                BatteryInfo.Add(new BatteryProperty { Description = desc, Value = "Loading..." });
            }

            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
            _ = InitializeBatteryDataAsync();

            _fastTimer.Interval = TimeSpan.FromSeconds(AppSettings.RefreshSpeed);
            _fastTimer.Tick += (_, __) => RefreshLiveBattery();

            _slowTimer.Interval = TimeSpan.FromMinutes(2);
            _slowTimer.Tick += async (_, __) => await RefreshReport();

            _fastTimer.Start();
            _slowTimer.Start();
        }
        private void AggregateBattery_ReportUpdated(Battery sender, object args)
        {
            this.DispatcherQueue.TryEnqueue(() =>
            {
                RefreshLiveBattery();
            });
        }
        private async Task InitializeBatteryDataAsync()
        {
            await RefreshReport();
            RefreshLiveBattery();
        }
        private void RefreshLiveBattery()
        {
            var updatedLiveItems = _provider.GetLiveBatteryInfo();

            foreach (var item in updatedLiveItems)
            {
                var existing = BatteryInfo.FirstOrDefault(x => x.Description == item.Description);
                if (existing != null) existing.Value = item.Value;
            }
        }
        private async Task RefreshReport()
        {
            var updatedReportItems = await _provider.GetBatteryInfoAsync();

            foreach (var item in updatedReportItems)
            {
                var existing = BatteryInfo.FirstOrDefault(x => x.Description == item.Description);
                if (existing != null) existing.Value = item.Value;
            }
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e) { RefreshLiveBattery(); }
        public void UpdateRefreshSpeed() { _fastTimer.Interval = TimeSpan.FromSeconds(AppSettings.RefreshSpeed); }
    }
}