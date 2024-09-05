using System.ComponentModel.DataAnnotations;

namespace WebVanChuyen.Models.Models
{
    // Record list of path form start to end and cost
    public class WHRoute
    {
        [Key] public int RouteId { get; set; }

        [MinLength(1, ErrorMessage = "Chọn nơi bắt đầu")]
        public string StartPoint { get; set; }              // Point A, B = WarehouseId

        [MinLength(1, ErrorMessage = "Chọn nơi kết thúc")]
        public string EndPoint { get; set; }

        public string StopPointsList { get; set; }            // List of stop point split by ,
        public double TotalCost { get; set; }
        public int TotalShipCost { get; set; }
    }

    // Record cost between 2 adjacent warehouse
    public class WHPath
    {
        [Key] public int PathId { get; set; }

        [MinLength(1, ErrorMessage = "Chọn nơi bắt đầu")]
        public string StartPoint { get; set; }      // Start and End is Warehouse

        [MinLength(1, ErrorMessage = "Chọn nơi kết thúc")]
        public string EndPoint { get; set; }

        [Range(1, 1000, ErrorMessage = "Giá trị phải lớn hơn 1")]
        public double Cost { get; set; }

        [Range(1, 1000, ErrorMessage = "Giá trị phải lớn hơn 1")]
        public int ShipCost { get; set; }
    }

    public class Node
    {
        public string NodeId { get; set; }                              // Is WarehouseId
        public string? NearestNodeStart { get; set; } = string.Empty;    // Save nearest node
        public double? MinCost { get; set; } = null;                    // Cost to move
        public int ShipCost { get; set; } = 0;
        public bool Visited { get; set; } = false;                      // Check if aready check
        public List<WHPath> ListPaths { get; set; } = null;       // List of path conn to node
    }

    public class StartAndEnd
    {
        [MinLength(1, ErrorMessage = "Chọn nơi bắt đầu")]
        public string StartPoint { get; set; }

        [MinLength(1, ErrorMessage = "Chọn nơi kết thúc")]
        public string EndPoint { get; set; }
    }
}
