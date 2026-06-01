using System;
using Windows.Devices.Power;
using Windows.System.Power;

namespace Battery_Health_Viewer.Services
{
    public class BatteryService
    {
        // A class that gets the battery specs for BatteryInfoProvider.cs to use
        private readonly Battery _battery = Battery.AggregateBattery;

        public BatteryReport GetReport()
        {
            return _battery.GetReport();
        }
    }
}