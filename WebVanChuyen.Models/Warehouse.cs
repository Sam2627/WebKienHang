using System.ComponentModel.DataAnnotations;

namespace WebVanChuyen.Models.Models
{
    public class Warehouse
    {
        [Key] public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseDesc { get; set; } = string.Empty;
        public string WarehouseAddr { get; set; } = string.Empty;
        public string ProvinceId { get; set; }
        public string GPSCoordinates { get; set; } = string.Empty;   // GG map location node string value
    }

    public class Province
    {
        [Key] public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }

    public class District
    {
        [Key] public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceId { get; set; }
    }

    public class Commune
    {
        [Key] public int CommuneId { get; set; }
        public string CommuneName { get; set;}
        public int DistrictId { get; set; }
    }

    public class AddWarehouse
    {
        [Required(ErrorMessage = "Kho hàng phải có mã")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Mã kho hàng tối thiểu có 1 - 10 kí tự")]
        public string WarehouseId { get; set; }

        [Required(ErrorMessage = "Kho hàng phải có tên")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Tên kho hàng tối thiểu có 1 - 100 kí tự")]
        public string WarehouseName { get; set; } = string.Empty;
        public string WarehouseDesc { get; set; } = string.Empty;
        public string WarehouseAddr { get; set; } = string.Empty;

        [Required(ErrorMessage = "Chọn tỉnh/tp")]
        [StringLength(100, MinimumLength = 1)]
        public string ProvinceId { get; set; } = string.Empty;

        [Range(1, 100000, ErrorMessage = "Xin chọn quận/huyện")]
        public int DistrictId { get; set; } = 0;

        [Range(1, 500000, ErrorMessage = "Chọn phường/xã")]
        public int CommuneId { get; set; } = 0;

        public string GPSCoordinates { get; set; } = string.Empty;   // GG map location node string value
    }

}
