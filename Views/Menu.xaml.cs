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
                Description = "Battery Name",
                Value = "SurfaceBattery"
            });

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Manufacturer",
                Value = "DYN"
            });

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Serial Number",
                Value = "nah I ain't showing that"
            });

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Manufacture Date",
                Value = ""
            });

            BatteryInfo.Add(new BatteryProperty
            {
                Description = "Power State",
                Value = "Discharging, AC Power"
            });

            BatteryInfo.Add(new BatteryProperty { Description = "Current Capacity (in %)", Value = "49.4%" });
            BatteryInfo.Add(new BatteryProperty { Description = "Current Capacity Value", Value = "19,130 mWh" });
            BatteryInfo.Add(new BatteryProperty { Description = "Full Charge Capacity", Value = "38,720 mWh" });
            BatteryInfo.Add(new BatteryProperty { Description = "Designed Capacity", Value = "45,800 mWh" });
            BatteryInfo.Add(new BatteryProperty { Description = "Battery Health", Value = "84.5%" });
            BatteryInfo.Add(new BatteryProperty { Description = "Voltage", Value = "7,656 mV" });
            BatteryInfo.Add(new BatteryProperty { Description = "Charge/Discharge Rate", Value = "-15 mW" });
            BatteryInfo.Add(new BatteryProperty { Description = "Chemistry", Value = "Lithium Ion" });
            BatteryInfo.Add(new BatteryProperty { Description = "Charge Cycles Number", Value = "470" });
            BatteryInfo.Add(new BatteryProperty { Description = "Battery Temperature", Value = "" });
        }
    }
}