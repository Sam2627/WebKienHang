using CustomInputAndValidation.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace WebVanChuyen.Models.Models
{
    public class ShipmentTrip
    {
        [Key] public int ShipmentTripId { get; set; }
        public string TruckId { get; set; }
        public int Manager { get; set; }
        public int Driver1 { get; set; }
        public int? Driver2 { get; set; }
        public DateTime BeginT { get; set; }
        public DateTime? ArrivedT { get; set; }
        public string StartWH { get; set; }
        public string EndWH { get; set; }
        public int Status { get; set; }
    }

    public class ShipmentTripInfo
    {
        public int ShipmentId { get; set; }
        public string TruckId { get; set; }
        public string Driver1Name { get; set; }
        public string Driver2Name { get; set; }
        public DateTime BeginT { get; set; }
        public string StartWHName { get; set; }
        public string EndWHName { get; set; }
    }

    public enum ShipmentTripStatus 
    {
        Pending,
        Transporting,
        Arrived,
        Complete
    }

    public class UpdateShipmentTripStatus
    {
        public int ShipmentTripId { get; set; }
        public int Status { get; set; }
    }

    public class Truck
    {
        public string TruckId { get; set; }
        public string? LocaId { get; set;}
    }

    // Input
    public class AddShipmentTrip
    {
        // Shipment
        public int ShipmentTripId { get; set; }

        [Required(ErrorMessage = "Chọn xe tải")]
        [StringLength(10, MinimumLength = 1)]
        public string TruckId { get; set; }
        
        public int Manager {  get; set; }

        [Range(1, 100000, ErrorMessage = "Chọn tài xế chính")]
        public int Driver1 { get; set; }
        public int? Driver2 { get; set; }

        [Required]
        [DateTimeFromNow]
        public DateTime BeginT { get; set; }

        public DateTime? ArrivedT { get; set; } = null;
        public string StartWH { get; set; }      // Noi bat dau
        public string EndWH { get; set; }        // Diem den
        public ShipmentTripStatus Status { get; set; } = ShipmentTripStatus.Pending;

        // Add validation attributes
        [Required(ErrorMessage = "Chọn tỉnh/tp")]
        [StringLength(4, MinimumLength = 1)]
        public string ProvinceId { get; set; }
    }

}
