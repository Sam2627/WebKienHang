using System.ComponentModel.DataAnnotations;

namespace WebVanChuyen.Models.Models
{
    public class Employee
    {
        [Key] public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int PositionId { get; set; }
        public string WWarehouseId { get; set; }
        public string? LocaId { get; set; }

        //public virtual EmployeePosition Position { get; set; }
        //public virtual Warehouse? Warehouse { get; set; }
    }

    public class EmployeePosition
    {
        [Key] public int PositionId { get; set; }
        public string PositionName { get; set; }
    }

    public class EmployeeLocation
    {
        public int Employee { get; set; }
        public string Warehouse { get; set; }
    }

    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeePositionId { get; set; }
        public string WorkWarehouseId { get; set; }
    }
}
