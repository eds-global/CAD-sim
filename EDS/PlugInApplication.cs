using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using System.Net.NetworkInformation;
using System.Windows.Forms;

[assembly: ExtensionApplication(typeof(EDS.PlugInApplication))]

namespace EDS
{
    public class PlugInApplication : IExtensionApplication
    {
        string ExpectedMacAddress = null;

        public List<string> macAddresses = new List<string>
        {
            "80:B6:55:FC:E8:83",
            "DC:21:5C:06:08:A6",
            "D4:D8:53:22:17:9E",
            "50:28:4A:9F:31:F7",
            "E8:F4:08:EE:07:46",
            "04:EC:D8:33:2F:B1",
            "04:EC:D8:32:CD:5F",
            "3C:E9:F7:6D:65:19",
            "04:EC:D8:33:2D:DB",
            "E4:46:B0:3A:4C:81",
            "BC:03:58:72:B6:40",
            "BC:03:58:74:E9:E2",
            "B4:8C:9D:13:E2:7B",
            "F4:6D:3F:64:5A:A0",
            "F4:6D:3F:64:4D:CB",
            "F4:6D:3F:61:D9:83",
            "F4:6D:3F:60:5F:AE",
            "04:EC:D8:36:AE:02",
            "14:D4:24:17:03:51",
            "10:68:38:39:50:F2",
            "10:68:38:31:B3:86",
            "10:68:38:31:B4:0E",
            "10:68:38:31B3:D4",
            "10:68:38:31:B7:E4",
            "10:68:38:31:B3:91",
            "10:68:38:31:bf:e8",
            "F4:3B:D8:4D:31:59",
            "F4:3B:D8:4D:32:0D",
            "F4:3B:D8:4D:32:CB",
            "F0:C2:64:63:F7:2C",
            "28:D0:EA:62:FA:39",
            "3C:E9:F7:6E:04:C9",
            "C0:A5:E8:3B:CD:D3",
            "CC:5E:F8:D1:AA:D1",
            "50:84:92:CB:48:1E",
            "E4:0D:36:34:17:F0",
            "1C:CE:51:27:EB:56",
            "CC:47:40:B1:D5:26",
            "1C:CE:51:28:33:F2",
            "B8:1E:A4:D9:DE:F5",
            "B8:1E:A4:A1:9B:F3",
            "40:86:CB:20:6A:CE",
            "24:EE:9A:1D:E0:61",
            "18:93:41:D0:F6:15"
        };

        public void Initialize()
        {
            List<string> macAddressesWithDash = new List<string>();

            foreach (string mac in macAddresses)
            {
                macAddressesWithDash.Add(mac.Replace(":", "-"));
            }
            string currentMacAddress = GetMacAddress();

            if (macAddressesWithDash.Count > 0)
            {
                if (macAddressesWithDash.Contains(currentMacAddress))
                    ExpectedMacAddress = macAddressesWithDash.Find(mac => mac.Equals(currentMacAddress));
            }

            else if (macAddresses.Count > 0)
            {
                if (macAddresses.Contains(currentMacAddress))
                    ExpectedMacAddress = macAddresses.Find(mac => mac.Equals(currentMacAddress));
            }
            if (string.IsNullOrEmpty(ExpectedMacAddress))
            {
                MessageBox.Show("This installer can only be used on specific machines.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.Equals(currentMacAddress, ExpectedMacAddress, StringComparison.OrdinalIgnoreCase))
            {
                commands.EditorReactorOnOff();
                commands.EDS();
            }
            else
            {
                MessageBox.Show("This installer can only be used on specific machines.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        public void Terminate()
        {
            // Add your uninitialize code here.
        }

        private string GetMacAddress()
        {
            var macAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up &&
                              nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            if (macAddress != null && macAddress.Length > 0)
            {
                // Format MAC address for readability (e.g., 00-1A-2B-3C-4D-5E)
                return string.Join("-", Enumerable.Range(0, macAddress.Length / 2)
                    .Select(i => macAddress.Substring(i * 2, 2)));
            }

            return null;
        }
    }
}
