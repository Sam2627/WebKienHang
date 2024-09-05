using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services.Data
{
    public class ShipmentTripStateContainer
    {
        public ShipmentTripInfo ShipmentTripInfo { get; set; }
        public List<PackageLog> PackageLogs { get; set; }

        public event Action OnStateChange;

        public void SetValue(ShipmentTripInfo shipmentTripInfo, List<PackageLog> packageLogs)
        {
            ShipmentTripInfo = shipmentTripInfo;
            PackageLogs = packageLogs;

            NotifyStateChanged();
        }

        private async void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
