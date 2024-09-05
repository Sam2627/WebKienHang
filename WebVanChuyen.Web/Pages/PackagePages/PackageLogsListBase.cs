using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.PackagePages
{
    public class PackageLogsListBase : ComponentBase
    {
        [Inject] private IPackageService PackageService { get; set; }

        // Package
        protected List<PackageLog>? Logs { get; set; } = new List<PackageLog>();

        // Input value
        protected string InputSearch { get; set; } = string.Empty;

        // Other variable
        protected string ErrorMessage { get; set; } = string.Empty;
        protected string PackageStatus {  get; set; } = string.Empty;
        protected bool LoadingPage { get; set; } = false;
        protected bool IsPhone {  get; set; } = false ;


        protected override async Task OnInitializedAsync()  
        {

        }

        protected async Task HandleValidSubmit()
        {
            LoadingPage = true;
            await Task.Delay(1500);

            if (CheckInt(InputSearch))
            {
                if (IsPhone) await GetPackageLogsByPhone(InputSearch);
                else
                {
                    int packageId = int.Parse(InputSearch);
                    await GetPackaLogsByPackage(packageId);
                }

                if (Logs.Count > 0)
                {
                    ErrorMessage = string.Empty;
                }
                else ErrorMessage = "Không tìm thấy đơn vận chuyển";
            }
            else
            {
                if(IsPhone) ErrorMessage = "Số điện thoại không hợp lệ hoặc không tìm thấy";
                else ErrorMessage = "Mã đơn vận chuyển không hợp lệ hoặc không tìm thấy";
            }

            LoadingPage = false;

            StateHasChanged();
        }

        protected bool CheckInt(string packageId)
        {
            if (int.TryParse(packageId, out int value))
            {
                return true;
            }
            return false;
        }

        protected async Task GetPackaLogsByPackage(int packageId)
        {
            Logs.Clear();
            Logs = (await PackageService.GetPackageLogsByPackage(packageId))
                .OrderByDescending(p => p.PackageLogId).ToList();
        }

        protected async Task GetPackageLogsByPhone(string phoneNum)
        {
            Logs.Clear();
            Logs = (await PackageService.GetPackageLogsByPhone(phoneNum))
                .OrderByDescending(p => p.PackageLogId).ToList();
        }

        protected async Task GetPackgeStatus(int packageId)
        {
            var statusIndex = (await PackageService.GetPackage(packageId)).Status;
            PackageStatus = ((PackageStatus)statusIndex).ToString();
        }

        public void ReloadInput()
        {
            StateHasChanged();
        }
    }
}
