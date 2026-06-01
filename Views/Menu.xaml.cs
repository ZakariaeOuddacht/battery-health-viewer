using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Battery_Health_Viewer.Services;

namespace Battery_Health_Viewer.Views
{
    public sealed partial class Menu : Page
    {
        public ObservableCollection<BatteryProperty> BatteryInfo { get; set; }

        private readonly BatteryInfoProvider _provider = new();
        private readonly DispatcherTimer _timer = new();

        public Menu()
        {
            this.InitializeComponent();

            BatteryInfo = _provider.GetBatteryInfo();

            SetupLiveUpdates();
        }

        private void SetupLiveUpdates()
        {
            _timer.Interval = TimeSpan.FromSeconds(0.5);

            _timer.Tick += (s, e) =>
            {
                var updated = _provider.GetBatteryInfo();

                BatteryInfo.Clear();
                foreach (var item in updated)
                    BatteryInfo.Add(item);
            };

            _timer.Start();
        }
    }
}