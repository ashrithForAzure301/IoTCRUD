using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using IoTCRUD.Models;

namespace IoTCRUD.Repositories
{
    public class DeviceTwinRepository
    {
        private static string connectionString = "HostName=ashrithhub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=q5vezRqSubOxhqmkU3jEkKyMPk4NpZRyIpZ97BDfBrc=";

        public static async Task<bool> IsDeviceAvailable(string deviceId)
        {
            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            Microsoft.Azure.Devices.Device device = await registryManager.GetDeviceAsync(deviceId);
            if (device.Status == DeviceStatus.Enabled)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> UpdateReportedPropertiesAsync(DeviceTwin properties, string deviceId)
        {
            var deviceClient = DeviceClient.CreateFromConnectionString(connectionString, deviceId);
            {
                if (await IsDeviceAvailable(deviceId))
                {
                    var reportedProperties = new TwinCollection();
                    reportedProperties[properties.Key] = properties.Value;
                    await deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
                    //var twin = await registryManager.GetTwinAsync(deviceId);
                    //return twin;
                    return "Reported Properties Updated Successfully";
                }
                return "Device is disabled";
            }
        }
        public async Task<string> UpdateDesiredPropertiesAsync(DeviceTwin properties, string deviceId)
        {
            var registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            {
                if (await IsDeviceAvailable(deviceId))
                {
                    var desiredProperties = new TwinCollection();
                    desiredProperties[properties.Key] = properties.Value;

                    var twin = await registryManager.GetTwinAsync(deviceId);
                    twin.Properties.Desired = desiredProperties;

                    await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);
                    //return twin;
                    return "Desired Properties Updated Successfully";
                }
                return "Device is disabled";
            }
        }
    }
}
