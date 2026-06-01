using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;

namespace Battery_Health_Viewer.Views
{
    public sealed partial class Menu : Page
    {
        public ObservableCollection<BatteryProperty> BatteryInfo { get; } = new();

        public Menu()
        {
            this.InitializeComponent();

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Manufacturer",
                Value = "Microsoft"
            });

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Design Capacity",
                Value = "45,000 mWh"
            });

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Full Charge Capacity",
                Value = "40,800 mWh"
            });

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Battery Health",
                Value = "90.6%"
            });

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Status",
                Value = "Discharging"
            });
        }
    }
}