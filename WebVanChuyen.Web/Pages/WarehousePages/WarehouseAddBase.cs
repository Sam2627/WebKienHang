using AutoMapper;
using Microsoft.AspNetCore.Components;
using WebVanChuyen.Models.Models;
using WebVanChuyen.Web.Services;

namespace WebVanChuyen.Web.Pages.WarehousePages
{
    public class WarehouseAddBase : ComponentBase
    {
        [Inject] private IWarehouseService WarehouseService { get; set; }
        [Inject] private IMapper Mapper { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected bool ReloadDistricts { get; set; } = true;
        protected List<Province> Provinces { get; set; } = new List<Province>();
        protected List<District> Districts { get; set; } = new List<District>();
        protected List<District> LoadDistricts { get; set; } = new List<District>();
        protected Warehouse Warehouse { get; set; } = new Warehouse();
        protected AddWarehouse AddWarehouse { get; set; } = new AddWarehouse();

        // Check Warehouse Id
        protected bool IsExist { get; set; }
        protected string IsExistMessage { get; set; }


        protected override async Task OnInitializedAsync()
        {
            // Load relate to Warehouse
            Provinces = await WarehouseService.GetProvinces();
            Districts = await WarehouseService.GetDistricts();
        }

        protected async Task HandleValidSubmit()
        {
            IsExist = await WarehouseService.CheckWarehouseExist(AddWarehouse.WarehouseId);
            if (!IsExist)
            {
                Mapper.Map(AddWarehouse, Warehouse);
                await WarehouseService.CreateWarehouse(Warehouse);

                NavigationManager.NavigateTo("/warehouses", true);
            }
            else { IsExistMessage = "Mã kho hàng đã tồn tại"; }
        }

        protected void OnChangeProvince()
        {
            ReloadDistricts = true;
            AddWarehouse.DistrictId = 0;
            LoadDistricts.Clear();

            StateHasChanged();
        }

        // Reload Districct list if Province change
        protected void OnClickDistrict()
        {
            if (ReloadDistricts && AddWarehouse.ProvinceId != "")
            {
                ReloadDistricts = false;
                LoadDistricts = Districts.Where(district => district.ProvinceId == AddWarehouse.ProvinceId).ToList();

                StateHasChanged();
            }
        }
    }
}
