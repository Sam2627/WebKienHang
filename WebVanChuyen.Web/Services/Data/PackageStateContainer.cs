using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Web.Services.Data
{
    public class PackageStateContainer
    {
        public string EmployeeName {  get; set; }
        public string WarehouseName {  get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string CommuneName { get; set; }
        public Package Package {  get; set; }

        public event Action OnStateChange;

        public void SetValue(Package value, string employeeName, string warehouseName, string provinceName, string districtName, string communeName)
        {
            Package = value;
            EmployeeName = employeeName;
            WarehouseName = warehouseName;
            ProvinceName = provinceName;
            DistrictName = districtName;
            CommuneName = communeName;

            NotifyStateChanged();
        }

        private async void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
