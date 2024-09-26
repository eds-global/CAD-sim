using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace LicenseValidator
{
    public class LicenseValidator
    {
        private const string ExpectedMacAddress = "C8:5A:CF:42:EB:03"; // Replace with your target MAC address

        [CustomAction]
        public static ActionResult ValidateMacAddress(Session session)
        {
            try
            {
                string currentMacAddress = GetMacAddress();

                session.Log($"Current MAC Address: {currentMacAddress}");
                session.Log($"Expected MAC Address: {ExpectedMacAddress}");

                // Compare MAC addresses (ignoring case)
                if (string.Equals(currentMacAddress, ExpectedMacAddress, StringComparison.OrdinalIgnoreCase))
                {
                    session.Log("MAC address validated successfully.");
                    return ActionResult.Success;
                }
                else
                {
                    session.Log("MAC address validation failed.");
                    MessageBox.Show("This installer can only be used on specific machines.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ActionResult.Failure;
                }
            }
            catch (Exception ex)
            {
                session.Log($"Error during MAC address validation: {ex.Message}");
                MessageBox.Show("This installer can only be used on specific machines error while installing.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ActionResult.Failure;
            }
        }

        // Function to get the MAC address
        private static string GetMacAddress()
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

            throw new Exception("No valid MAC address found.");
        }
    }
}

