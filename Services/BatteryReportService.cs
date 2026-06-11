using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Battery_Health_Viewer.Services
{
    public class BatteryReportService
    {
        private static readonly string ReportPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "BHPspecific.xml");

        private static readonly TimeSpan CacheTime = TimeSpan.FromHours(1);

        public async Task<BatteryReportInfo> LoadAsync()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ReportPath)!);

            if (!File.Exists(ReportPath) ||
                DateTime.Now - File.GetLastWriteTime(ReportPath) > CacheTime)
            {
                await GenerateReportAsync();
            }

            return ParseReport();
        }

        private async Task GenerateReportAsync()
        {
            await Task.Run(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "powercfg",
                    Arguments = $"/batteryreport /xml /output \"{ReportPath}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using var process = Process.Start(psi);
                process?.WaitForExit();
            });
        }

        private BatteryReportInfo ParseReport()
        {
            var result = new BatteryReportInfo();

            try
            {
                var doc = XDocument.Load(ReportPath);
                XNamespace ns = "http://schemas.microsoft.com/battery/2012";

                var battery = doc.Root?
                    .Element(ns + "Batteries")?
                    .Element(ns + "Battery");

                if (battery == null)
                    return result;

                result.Manufacturer =
                    battery.Element(ns + "Manufacturer")?.Value ?? "Unknown";

                result.SerialNumber =
                    battery.Element(ns + "SerialNumber")?.Value ?? "Unknown";

                result.ManufactureDate =
                    string.IsNullOrWhiteSpace(battery.Element(ns + "ManufactureDate")?.Value)
                        ? "Not Available"
                        : battery.Element(ns + "ManufactureDate")!.Value;

                result.CycleCount =
                    battery.Element(ns + "CycleCount")?.Value ?? "Unknown";
            }
            catch
            {
                // Silent failure, that's how corporations do it, I don't blame them ¯\_('_')_/¯
            }

            return result;
        }
    }
}