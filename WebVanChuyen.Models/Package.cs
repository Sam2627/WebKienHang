using System.ComponentModel.DataAnnotations;

namespace WebVanChuyen.Models.Models
{
    public class Package
    {
        [Key] public int PackageId { get; set; }
        public string? PackageDesc { get; set; } 
        public int ReceptionistId { get; set; }           
        public int? ShipperId { get; set; }
        public int Status { get; set; }
        public string? LocaId { get; set; }         // Where last storage packge in aka WarehouseId
        public double Weight { get; set; }
        public int? RouteId { get; set; }

        // Sender
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string SenderAddr { get; set; }

        // Receiver
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverAddr { get; set; }
        public string? ReceiverId { get; set; }

        // Payment
        public int Collect { get; set; }
        public int ShipCost { get; set; }

        //public PaymentStatus PaymentStatus { get; set; }
        public int PayStatus { get; set; }
    }

    public class PackageWeight
    {
        [Key] public double Weight { get; set; }
        public int WCost { get; set; }
    }

    public class PackageDetails
    {
        [Key] public int PackageDetsId { get; set; }
        public int PackageId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        //public virtual Product vProduct { get; set; }
    }

    public class PackageLog
    {
        [Key] public int PackageLogId { get; set; }
        public int? ShipmentTripId { get; set; } = null;
        public string? TruckId { get; set; } = null;
        public int PackageId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string LogNote { get; set; } = string.Empty;
    }

    public enum PackageStatus
    {
        Stored,             // package store in start or middle warehouse of assign route
        Tranporting,        // package in transport truck 
        WaitingDelivery,    // package in destination warehouse and wait for shipper delivery
        Delivered,          // package is delivered by shipper
        Complete,           // package is confirm delivery by customer
        Cancel
    }

    public enum PaymentStatus
    {
        Unpaid,         // Not pay ship cost
        Paid            // Already pay ship cost
    }


    // Input
    public class AddPackage
    {
        public int PackageId { get; set; }
        public string? PackageDesc { get; set; }
        public int ReceptionistId { get; set; }
        public int? ShipperId { get; set; } = null;
        public PackageStatus Status { get; set; } = PackageStatus.Stored;
        public string? LocaId { get; set; }         // Where last storage packge in aka WarehouseId

        [Range(0, 10000, ErrorMessage = "Chọn cân nặng")]
        public double Weight { get; set; } = 0;

        public int? RouteId { get; set; } = null;

        // Sender
        [Required(ErrorMessage = "Phải có tên người gửi")]
        [StringLength(100, MinimumLength = 2)]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Phải có sdt người gửi")]
        [StringLength(12, MinimumLength = 8)]
        public string SenderPhone { get; set; }

        [Required(ErrorMessage = "Phải có địa chỉ người gửi")]
        [StringLength(100, MinimumLength = 2)]
        public string SenderAddr { get; set; }

        // Receiver
        [Required(ErrorMessage = "Phải có tên người nhận")]
        [StringLength(100, MinimumLength = 2)]
        public string ReceiverName { get; set; }

        [Required(ErrorMessage = "Phải có sdt người nhận")]
        [StringLength(12, MinimumLength = 8)]
        public string ReceiverPhone { get; set; }

        [Required(ErrorMessage = "Phải có địa chỉ người nhận")]
        [StringLength(100, MinimumLength = 2)]
        public string ReceiverAddr { get; set; }

        public string? ReceiverId { get; set; } = null;

        // Payment
        [Range(0, int.MaxValue, ErrorMessage = "Giá trị tối thiểu là 0")]
        public int Collect { get; set; } = 0;

        public int ShipCost { get; set; } = 0;

        [Range(0, 10, ErrorMessage = "Chọn lại phương thức thanh toán")]
        public PaymentStatus PayStatus { get; set; } = PaymentStatus.Unpaid;


        // Add validation attributes
        [Required(ErrorMessage = "Chọn tỉnh/tp")]
        [StringLength(100, MinimumLength = 1)] 
        public string ProvinceId { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Chọn quận/huyện")]
        public int DistrictId { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "Chọn phường/xã")]
        public int CommuneId { get; set; } = 0;
    }

    public class PackageRegister
    {
        public int PackageId { get; set; }
        public int ShipperId { get; set; }
    }

    public class PackageDelivery
    {
        public int PackageId { get; set; }
        public string ReceiverId { get; set; }
    }

    public class DeliveryConfirm
    {
        public int PackageId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage ="Độ dài phải trên 5 kí tự")]
        public string ReceiverId { get; set; }

        public bool IsConfirm { get; set; }
    }
}
