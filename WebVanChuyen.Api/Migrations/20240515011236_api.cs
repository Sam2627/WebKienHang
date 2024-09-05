using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVanChuyen.Api.Migrations
{
    /// <inheritdoc />
    public partial class api : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Commune",
                columns: table => new
                {
                    CommuneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommuneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commune", x => x.CommuneId);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    WWarehouseId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocaId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePosition",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePosition", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceptionistId = table.Column<int>(type: "int", nullable: false),
                    ShipperId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LocaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: true),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAddr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverAddr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Collect = table.Column<int>(type: "int", nullable: false),
                    ShipCost = table.Column<int>(type: "int", nullable: false),
                    PayStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.PackageId);
                });

            migrationBuilder.CreateTable(
                name: "PackageDetail",
                columns: table => new
                {
                    PackageDetsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageDetail", x => x.PackageDetsId);
                });

            migrationBuilder.CreateTable(
                name: "PackageLog",
                columns: table => new
                {
                    PackageLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentTripId = table.Column<int>(type: "int", nullable: true),
                    TruckId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogNote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageLog", x => x.PackageLogId);
                });

            migrationBuilder.CreateTable(
                name: "PackageWeight",
                columns: table => new
                {
                    Weight = table.Column<double>(type: "float", nullable: false),
                    WCost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageWeight", x => x.Weight);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCateId);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProvinceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentTrip",
                columns: table => new
                {
                    ShipmentTripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manager = table.Column<int>(type: "int", nullable: false),
                    Driver1 = table.Column<int>(type: "int", nullable: false),
                    Driver2 = table.Column<int>(type: "int", nullable: true),
                    BeginT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivedT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartWH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndWH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentTrip", x => x.ShipmentTripId);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    TruckId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocaId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.TruckId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseAddr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPSCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "WHPath",
                columns: table => new
                {
                    PathId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    ShipCost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHPath", x => x.PathId);
                });

            migrationBuilder.CreateTable(
                name: "WHRoute",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StopPointsList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    TotalShipCost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHRoute", x => x.RouteId);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Thuc an" },
                    { 2, "Thuc uong" },
                    { 3, "De vo" },
                    { 4, "Dong lanh" }
                });

            migrationBuilder.InsertData(
                table: "Commune",
                columns: new[] { "CommuneId", "CommuneName", "DistrictId" },
                values: new object[,]
                {
                    { 27826, "Thi tran Tan Thanh", 799 },
                    { 27829, "Xa Bac Hoa", 799 },
                    { 27832, "Xa Hau Thanh Tay", 799 },
                    { 27835, "Xa Nhon Hoa Lap", 799 },
                    { 27838, "Xa Tan Lap", 799 },
                    { 27841, "Xa Hau Thanh Dong", 799 },
                    { 27844, "Xa Nhon Hoa", 799 },
                    { 27847, "Xa Kien Binh", 799 },
                    { 27850, "Xa Tan Thanh", 799 },
                    { 27853, "Xa Tan Binh", 799 },
                    { 27856, "Xa Tan Ninh", 799 },
                    { 27859, "Xa Nhon Ninh", 799 },
                    { 27862, "Xa Tan Hoa", 799 },
                    { 28519, "Thi tran Tan Hiep", 821 },
                    { 28522, "Xa Tan Hoi Dong", 821 },
                    { 28525, "Xa Tan Huong", 821 },
                    { 28528, "Xa Tan Ly Dong", 821 },
                    { 28531, "Xa Tan Ly Tay", 821 },
                    { 28534, "Xa Than Cuu Nghia", 821 },
                    { 28537, "Xa Tam Hiep", 821 },
                    { 28540, "Xa Diem Hy", 821 },
                    { 28543, "Xa Nhi Binh", 821 },
                    { 28546, "Xa Duong Diem", 821 },
                    { 28549, "Xa Dong Hoa", 821 },
                    { 28552, "Xa Long Dinh", 821 },
                    { 28555, "Xa Huu Dao", 821 },
                    { 28558, "Xa Long An", 821 },
                    { 28561, "Xa Long Hung", 821 },
                    { 28564, "Xa Binh Trung", 821 },
                    { 28570, "Xa Thanh Phu", 821 },
                    { 28573, "Xa Ban Long", 821 },
                    { 28576, "Xa Vinh Kim", 821 },
                    { 28579, "Xa Binh Duc", 821 },
                    { 28582, "Xa Song Thuan", 821 },
                    { 28585, "Xa Kim Son", 821 },
                    { 28588, "Xa Phu Phong", 821 },
                    { 28801, "Thi tran Chau Thanh", 831 },
                    { 28804, "Xa Tan Thach", 831 },
                    { 28807, "Xa Quoi Son", 831 },
                    { 28810, "Xa An Khanh", 831 },
                    { 28813, "Xa Giao Long", 831 },
                    { 28819, "Xa Phu Tuc", 831 },
                    { 28822, "Xa Phu Đuc", 831 },
                    { 28825, "Xa Phu An Hoa", 831 },
                    { 28828, "Xa An Phuoc", 831 },
                    { 28831, "Xa Tam Phuoc", 831 },
                    { 28834, "Xa Thanh Trieu", 831 },
                    { 28837, "Xa Tuong Đa", 831 },
                    { 28840, "Xa Tan Phu", 831 },
                    { 28843, "Xa Quoi Thanh", 831 },
                    { 28846, "Xa Phuoc Thanh", 831 },
                    { 28849, "Xa An Hoa", 831 },
                    { 28852, "Xa Tien Long", 831 },
                    { 28855, "Xa An Hiep", 831 },
                    { 28858, "Xa Huu Đinh", 831 },
                    { 28861, "Thi tran Tien Thuy", 831 },
                    { 28864, "Xa Son Hoa", 831 },
                    { 29236, "Phuong 4", 842 },
                    { 29239, "Phuong 1", 842 },
                    { 29242, "Phuong 3", 842 },
                    { 29245, "Phuong 2", 842 },
                    { 29248, "Phuong 5", 842 },
                    { 29251, "Phuong 6", 842 },
                    { 29254, "Phuong 7", 842 },
                    { 29257, "Phuong 8", 842 },
                    { 29260, "Phuong 9", 842 },
                    { 29263, "Xa Long Đuc", 842 },
                    { 29542, "Phuong 9", 855 },
                    { 29545, "Phuong 5", 855 },
                    { 29548, "Phuong 1", 855 },
                    { 29551, "Phuong 2", 855 },
                    { 29554, "Phuong 4", 855 },
                    { 29557, "Phuong 3", 855 },
                    { 29560, "Phuong 8", 855 },
                    { 29563, "Phuong Tan Ngai", 855 },
                    { 29566, "Phuong Tan Hoa", 855 },
                    { 29569, "Phuong Tan Hoi", 855 },
                    { 29572, "Phuong Truong An", 855 },
                    { 29902, "Phuong 3", 867 },
                    { 29905, "Phuong 1", 867 },
                    { 29908, "Phuong 4", 867 },
                    { 29911, "Phuong 2", 867 },
                    { 29914, "Xa Tan Khanh Dong", 867 },
                    { 29917, "Phuong Tan Quy Dong", 867 },
                    { 29919, "Phuong An Hoa", 867 },
                    { 29920, "Xa Tan Quy Tay", 867 },
                    { 29923, "Xa Tan Phu Dong", 867 },
                    { 30280, "Phuong My Binh", 883 },
                    { 30283, "Phuong My Long", 883 },
                    { 30285, "Phuong Dong Xuyen", 883 },
                    { 30286, "Phuong My Xuyen", 883 },
                    { 30289, "Phuong Binh Duc", 883 },
                    { 30292, "Phuong Binh Khanh", 883 },
                    { 30295, "Phuong My Phuoc", 883 },
                    { 30298, "Phuong My Quy", 883 },
                    { 30301, "Phuong My Thoi", 883 },
                    { 30304, "Phuong My Thạnh", 883 },
                    { 30307, "Phuong My Hoa", 883 },
                    { 30310, "Xa My Khanh", 883 },
                    { 30313, "Xa My Hoa Hung", 883 },
                    { 30730, "Phuong Vinh Thanh Van", 899 },
                    { 30733, "Phuong Vinh Thanh", 899 },
                    { 30736, "Phuong Vinh Quang", 899 },
                    { 30739, "Phuong Vinh Hiep", 899 },
                    { 30742, "Phuong Vinh Bao", 899 },
                    { 30745, "Phuong Vinh Lac", 899 },
                    { 30748, "Phuong An Hoa", 899 },
                    { 30751, "Phuong An Binh", 899 },
                    { 30754, "Phuong Rach Soi", 899 },
                    { 30757, "Phuong Vinh Loi", 899 },
                    { 30760, "Phuong Vinh Thong", 899 },
                    { 30763, "Xa Phi Thong", 899 },
                    { 31117, "Phuong Cai Khe", 916 },
                    { 31120, "Phuong An Hoa", 916 },
                    { 31123, "Phuong Thoi Binh", 916 },
                    { 31126, "Phuong An Nghiep", 916 },
                    { 31129, "Phuong An Cu", 916 },
                    { 31135, "Phuong Tan An", 916 },
                    { 31141, "Phuong An Phu", 916 },
                    { 31144, "Phuong Xuan Khanh", 916 },
                    { 31147, "Phuong Hung Loi", 916 },
                    { 31149, "Phuong An Khanh", 916 },
                    { 31150, "Phuong An Binh", 916 },
                    { 31153, "Phuong Chau Van Liem", 917 },
                    { 31154, "Phuong Thoi Hoa", 917 },
                    { 31156, "Phuong Thoi Long", 917 },
                    { 31157, "Phuong Long Hung", 917 },
                    { 31159, "Phuong Thoi An", 917 },
                    { 31162, "Phuong Phuoc Thoi", 917 },
                    { 31165, "Phuong Truong Lac", 917 },
                    { 31168, "Phuong Binh Thuy", 918 },
                    { 31169, "Phuong Tra An", 918 },
                    { 31171, "Phuong Tra Noc", 918 },
                    { 31174, "Phuong Thoi An Dong", 918 },
                    { 31177, "Phuong An Thoi", 918 },
                    { 31178, "Phuong Bui Huu Nghia", 918 },
                    { 31180, "Phuong Long Hoa", 918 },
                    { 31183, "Phuong Long Tuyen", 918 },
                    { 31186, "Phuong Le Binh", 919 },
                    { 31189, "Phuong Hung Phu", 919 },
                    { 31192, "Phuong Hung Thanh", 919 },
                    { 31195, "Phuong Ba Lang", 919 },
                    { 31198, "Phuong Thuong Thanh", 919 },
                    { 31201, "Phuong Phu Thu", 919 },
                    { 31204, "Phuong Tan Phu", 919 },
                    { 31207, "Phuong Thot Not", 923 },
                    { 31210, "Phuong Thoi Thuan", 923 },
                    { 31211, "Xa Vinh Binh", 924 },
                    { 31212, "Phuong Thuan An", 923 },
                    { 31213, "Phuong Tan Loc", 923 },
                    { 31216, "Phuong Trung Nhut", 923 },
                    { 31217, "Phuong Thanh Hoa", 923 },
                    { 31219, "Phuong Trung Kien", 923 },
                    { 31222, "Xa Trung An", 925 },
                    { 31225, "Xa Trung Thanh", 925 },
                    { 31227, "Phuong Tan Hung", 923 },
                    { 31228, "Phuong Thuan Hung", 923 },
                    { 31231, "Thi tran Thanh An", 924 },
                    { 31232, "Thi tran Vinh Thanh", 924 },
                    { 31234, "Xa Thanh My", 924 },
                    { 31237, "Xa Vinh Trinh", 924 },
                    { 31240, "Xa Thanh An", 924 },
                    { 31241, "Xa Thanh Tien", 924 },
                    { 31243, "Xa Thanh Thang", 924 },
                    { 31244, "Xa Thanh Loi", 924 },
                    { 31246, "Xa Thanh Quoi", 924 },
                    { 31249, "Xa Thanh Phu", 925 },
                    { 31252, "Xa Thanh Loc", 924 },
                    { 31255, "Xa Trung Hung", 925 },
                    { 31258, "Thi tran Thoi Lai", 927 },
                    { 31261, "Thi tran Co Do", 925 },
                    { 31264, "Xa Thoi Hung", 925 },
                    { 31267, "Xa Thoi Thanh", 927 },
                    { 31268, "Xa Tan Thanh", 927 },
                    { 31270, "Xa Xuan Thang", 927 },
                    { 31273, "Xa Dong Hiep", 925 },
                    { 31274, "Xa Dong Thang", 925 },
                    { 31276, "Xa Thoi Dong", 925 },
                    { 31277, "Xa Thoi Xuân", 925 },
                    { 31279, "Xa Dong Bình", 927 },
                    { 31282, "Xa Dong Thuan", 927 },
                    { 31285, "Xa Thoi Tan", 927 },
                    { 31286, "Xa Truong Thang", 927 },
                    { 31288, "Xa Dinh Mon", 927 },
                    { 31291, "Xa Truong Thanh", 927 },
                    { 31294, "Xa Truong Xuan", 927 },
                    { 31297, "Xa Truong Xuan A", 927 },
                    { 31298, "Xa Truong Xuan B", 927 },
                    { 31299, "Thi tran Phong Dien", 926 },
                    { 31300, "Xa Nhon Ai", 926 },
                    { 31303, "Xa Giai Xuan", 926 },
                    { 31306, "Xa Tan Thoi", 926 },
                    { 31309, "Xa Truong Long", 926 },
                    { 31312, "Xa My Khanh", 926 },
                    { 31315, "Xa Nhon Nghia", 926 },
                    { 31441, "Thi tran Nang Mau", 935 },
                    { 31444, "Xa Vi Trung", 935 },
                    { 31447, "Xa Vi Thuy", 935 },
                    { 31450, "Xa Vi Thang", 935 },
                    { 31453, "Xa Vinh Thuan Tay", 935 },
                    { 31456, "Xa Vinh Trung", 935 },
                    { 31459, "Xa Vinh Tuong", 935 },
                    { 31462, "Xa Vi Dong", 935 },
                    { 31465, "Xa Vi Thanh", 935 },
                    { 31468, "Xa Vi Binh", 935 },
                    { 31639, "Thi tran Long Phu", 946 },
                    { 31642, "Xa Song Phung", 946 },
                    { 31645, "Thi tran Dai Ngai", 946 },
                    { 31648, "Xa Hau Thanh", 946 },
                    { 31651, "Xa Long Duc", 946 },
                    { 31654, "Xa Truong Khanh", 946 },
                    { 31657, "Xa Phu Huu", 946 },
                    { 31660, "Xa Tan Hung", 946 },
                    { 31663, "Xa Chau Khanh", 946 },
                    { 31666, "Xa Tan Thanh", 946 },
                    { 31669, "Xa Long Phu", 946 },
                    { 31894, "Xa Vinh Hung", 958 },
                    { 31897, "Xa Vinh Hung A", 958 },
                    { 31900, "Thi tran Chau Hung", 958 },
                    { 31903, "Xa Chau Hung A", 958 },
                    { 31906, "Xa Hung Thanh", 958 },
                    { 31909, "Xa Hung Hoi", 958 },
                    { 31912, "Xa Chau Thoi", 958 },
                    { 31921, "Xa Long Thanh", 958 },
                    { 31999, "Phuong 9", 964 },
                    { 32002, "Phuong 4", 964 },
                    { 32005, "Phuong 1", 964 },
                    { 32008, "Phuong 5", 964 },
                    { 32011, "Phuong 2", 964 },
                    { 32014, "Phuong 8", 964 },
                    { 32017, "Phuong 6", 964 },
                    { 32020, "Phuong 7", 964 },
                    { 32022, "Phuong Tan Xuyen", 964 },
                    { 32023, "Xa An Xuyen", 964 },
                    { 32025, "Phuong Tan Thanh", 964 },
                    { 32026, "Xa Tan Thanh", 964 },
                    { 32029, "Xa Tac Van", 964 },
                    { 32032, "Xa Ly Van Lam", 964 },
                    { 32035, "Xa Dinh Binh", 964 },
                    { 32038, "Xa Hoa Thanh", 964 },
                    { 32041, "Xa Hoa Tan", 964 }
                });

            migrationBuilder.InsertData(
                table: "District",
                columns: new[] { "DistrictId", "DistrictName", "ProvinceId" },
                values: new object[,]
                {
                    { 799, "H. Tan Thanh", "LA" },
                    { 821, "H. Chau Thanh", "TG" },
                    { 831, "H. Chau Thanh", "BT" },
                    { 842, "Tp. Tra Vinh", "TV" },
                    { 855, "Tp. Vinh Long", "VL" },
                    { 867, "Tp. Sa Dec", "DT" },
                    { 883, "Tp. Long Xuyen", "AG" },
                    { 899, "Tp. Rach Gia", "KG" },
                    { 916, "Q. Ninh Kieu", "CT" },
                    { 917, "Q. O Mon", "CT" },
                    { 918, "Q. Binh Thuy", "CT" },
                    { 919, "Q. Cai Rang", "CT" },
                    { 923, "Q. Thot Not", "CT" },
                    { 924, "H. Vinh Thanh", "CT" },
                    { 925, "H. Co Do", "CT" },
                    { 926, "H. Phong Dien", "CT" },
                    { 927, "H. Thoi Lai", "CT" },
                    { 935, "H. Vi Thuy", "HG" },
                    { 946, "H. Long Phu", "ST" },
                    { 958, "H. Vinh Loi", "BL" },
                    { 964, "Tp. Ca Mau", "CM" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "EmployeeName", "LocaId", "PositionId", "WWarehouseId" },
                values: new object[,]
                {
                    { 1, "Anh Dung", "CT-1", 2, "CT-1" },
                    { 2, "Thanh Hang", "CT-1", 3, "CT-1" },
                    { 3, "Quoc Bao", "CT-1", 4, "CT-1" },
                    { 4, "Ngoc Sa", "AG-1", 2, "AG-1" },
                    { 5, "Bao Loc", "AG-1", 3, "AG-1" },
                    { 6, "Thien Loc", "AG-1", 4, "AG-1" },
                    { 7, "Ngoc Bich", "CT-1", 3, "CT-1" },
                    { 8, "Minh Triet", "CT-1", 4, "CT-1" },
                    { 9, "Bao Loc", "AG-1", 4, "AG-1" },
                    { 10, "Ngoc Thanh", "BL-1", 2, "BL-1" },
                    { 11, "Minh Long", "BL-1", 3, "BL-1" },
                    { 12, "Be Ba", "BL-1", 4, "BL-1" },
                    { 13, "Huu Phuoc", "BL-1", 5, "BL-1" },
                    { 14, "Ngoc Trinh", "BT-1", 2, "BT-1" },
                    { 15, "Huu Nghia", "BT-1", 3, "BT-1" },
                    { 16, "Huynh Duc", "BT-1", 4, "BT-1" },
                    { 17, "Minh Thong", "BT-1", 5, "BT-1" },
                    { 18, "Binh Phuoc", "CM-1", 2, "CM-1" },
                    { 19, "Trung Tin", "CM-1", 3, "CM-1" },
                    { 20, "Quoc Bao", "CM-1", 4, "CM-1" },
                    { 21, "Minh Sang", "CM-1", 5, "CM-1" },
                    { 22, "Bao Thy", "DT-1", 2, "DT-1" },
                    { 23, "Minh Anh", "DT-1", 3, "DT-1" },
                    { 24, "Trung Thang", "DT-1", 4, "DT-1" },
                    { 25, "Thien Phuoc", "DT-1", 5, "DT-1" },
                    { 26, "Ngoc Bao", "HG-1", 2, "HG-1" },
                    { 27, "Lan Anh", "HG-1", 3, "HG-1" },
                    { 28, "Hoai Bao", "HG-1", 4, "HG-1" },
                    { 29, "Quoc Nghia", "HG-1", 4, "HG-1" },
                    { 30, "Thu Thuy", "KG-1", 2, "KG-1" },
                    { 31, "Bao An", "KG-1", 3, "KG-1" },
                    { 32, "Van Duc", "KG-1", 4, "KG-1" },
                    { 33, "Van Trong", "KG-1", 5, "KG-1" },
                    { 34, "Thu Ngoc", "LA-1", 2, "LA-1" },
                    { 35, "Bao Binh", "LA-1", 3, "LA-1" },
                    { 36, "Van Binh", "LA-1", 4, "LA-1" },
                    { 37, "Van Minh", "LA-1", 5, "LA-1" },
                    { 38, "Minh Duc", "ST-1", 2, "ST-1" },
                    { 39, "Hanh Ngoc", "ST-1", 3, "ST-1" },
                    { 40, "Binh Minh", "ST-1", 4, "ST-1" },
                    { 41, "Hoai Thu", "ST-1", 5, "ST-1" },
                    { 42, "Duc Tin", "TG-1", 2, "TG-1" },
                    { 43, "Ngoc Hoa", "TG-1", 3, "TG-1" },
                    { 44, "Minh Trong", "TG-1", 4, "TG-1" },
                    { 45, "Van Dung", "TG-1", 5, "TG-1" },
                    { 46, "Bao Long", "TV-1", 2, "TV-1" },
                    { 47, "Hoang Kim", "TV-1", 3, "TV-1" },
                    { 48, "Minh An", "TV-1", 4, "TV-1" },
                    { 49, "Quoc Huy", "TV-1", 5, "TV-1" },
                    { 50, "Long Duc", "VL-1", 2, "VL-1" },
                    { 51, "Kim Ngoc", "VL-1", 3, "VL-1" },
                    { 52, "Hoang Duc", "VL-1", 4, "VL-1" },
                    { 53, "Quoc Hai", "VL-1", 5, "VL-1" },
                    { 54, "Bao Minh", "HG-1", 5, "HG-1" },
                    { 55, "Vu Minh", "CT-1", 5, "CT-1" },
                    { 56, "Nghia Loc", "AG-1", 5, "AG-1" }
                });

            migrationBuilder.InsertData(
                table: "EmployeePosition",
                columns: new[] { "PositionId", "PositionName" },
                values: new object[,]
                {
                    { 1, "Unspecified" },
                    { 2, "Manager" },
                    { 3, "Staff" },
                    { 4, "Driver" },
                    { 5, "Shipper" }
                });

            migrationBuilder.InsertData(
                table: "PackageWeight",
                columns: new[] { "Weight", "WCost" },
                values: new object[,]
                {
                    { 0.5, 0 },
                    { 1.0, 1500 },
                    { 1.5, 3000 },
                    { 2.0, 4500 },
                    { 2.5, 5500 },
                    { 3.0, 6500 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "ProductDesc", "ProductName" },
                values: new object[,]
                {
                    { 1, "", "Gao" },
                    { 2, "", "Thit heo dong lanh" },
                    { 3, "", "Bia chai thuy tinh" },
                    { 4, "", "Bia lon" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "ProductCateId", "Category", "Product" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 4, 2 },
                    { 4, 2, 3 },
                    { 5, 3, 3 },
                    { 6, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Province",
                columns: new[] { "ProvinceId", "ProvinceName" },
                values: new object[,]
                {
                    { "AG", "An Giang" },
                    { "BL", "Bac Lieu" },
                    { "BT", "Ben Tre" },
                    { "CM", "Ca Mau" },
                    { "CT", "Can Tho" },
                    { "DT", "Dong Thap" },
                    { "HG", "Hau Giang" },
                    { "KG", "Kien Giang" },
                    { "LA", "Long An" },
                    { "ST", "Soc Trang" },
                    { "TG", "Tien Giang" },
                    { "TV", "Tra Vinh" },
                    { "VL", "Vinh Long" }
                });

            migrationBuilder.InsertData(
                table: "Truck",
                columns: new[] { "TruckId", "LocaId" },
                values: new object[,]
                {
                    { "62-1", "LA-1" },
                    { "62-2", "LA-1" },
                    { "63-1", "TG-1" },
                    { "63-2", "TG-1" },
                    { "64-1", "VL-1" },
                    { "64-2", "VL-1" },
                    { "65-01", "CT-1" },
                    { "65-02", "CT-1" },
                    { "66-1", "DT-1" },
                    { "66-2", "DT-1" },
                    { "67-01", "AG-1" },
                    { "67-02", "AG-1" },
                    { "68-1", "KG-1" },
                    { "68-2", "KG-1" },
                    { "69-1", "CM-1" },
                    { "69-2", "CM-1" },
                    { "71-1", "BT-1" },
                    { "71-2", "BT-1" },
                    { "83-1", "ST-1" },
                    { "83-2", "ST-1" },
                    { "84-1", "TG-1" },
                    { "84-2", "TG-1" },
                    { "94-1", "BL-1" },
                    { "94-2", "BL-1" },
                    { "95-1", "HG-1" },
                    { "95-2", "HG-1" }
                });

            migrationBuilder.InsertData(
                table: "WHPath",
                columns: new[] { "PathId", "Cost", "EndPoint", "ShipCost", "StartPoint" },
                values: new object[,]
                {
                    { 1, 63.950000000000003, "CT-1", 31975, "AG-1" },
                    { 2, 45.100000000000001, "DT-1", 22550, "AG-1" },
                    { 3, 75.0, "KG-1", 37500, "AG-1" },
                    { 4, 75.950000000000003, "CM-1", 37975, "BL-1" },
                    { 5, 64.25, "HG-1", 32125, "BL-1" },
                    { 6, 129.0, "KG-1", 64500, "BL-1" },
                    { 7, 46.049999999999997, "ST-1", 23025, "BL-1" },
                    { 8, 15.949999999999999, "TG-1", 7975, "BT-1" },
                    { 9, 48.350000000000001, "TV-1", 24175, "BT-1" },
                    { 10, 75.200000000000003, "VL-1", 37600, "BT-1" },
                    { 11, 121.5, "KG-1", 60750, "CM-1" },
                    { 12, 51.899999999999999, "DT-1", 25950, "CT-1" },
                    { 13, 43.450000000000003, "HG-1", 21725, "CT-1" },
                    { 14, 109.5, "KG-1", 54750, "CT-1" },
                    { 15, 38.149999999999999, "VL-1", 19075, "CT-1" },
                    { 16, 87.349999999999994, "LA-1", 43675, "DT-1" },
                    { 17, 85.049999999999997, "TG-1", 42525, "DT-1" },
                    { 18, 31.350000000000001, "VL-1", 15675, "DT-1" },
                    { 19, 66.5, "KG-1", 33250, "HG-1" },
                    { 20, 81.200000000000003, "ST-1", 40600, "HG-1" },
                    { 21, 79.5, "VL-1", 39750, "HG-1" },
                    { 22, 51.649999999999999, "TG-1", 25825, "LA-1" },
                    { 23, 57.700000000000003, "TV-1", 28850, "ST-1" },
                    { 24, 97.900000000000006, "VL-1", 48950, "ST-1" },
                    { 25, 77.900000000000006, "VL-1", 38950, "TG-1" },
                    { 26, 60.399999999999999, "VL-1", 30200, "TV-1" }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "WarehouseId", "GPSCoordinates", "ProvinceId", "WarehouseAddr", "WarehouseDesc", "WarehouseName" },
                values: new object[,]
                {
                    { "AG-1", "https://maps.app.goo.gl/uyH6tHXS8rkUWak16", "AG", "", "", "Kho An Giang" },
                    { "BL-1", "https://maps.app.goo.gl/BdPArUvT8Jbyk8dr9", "BL", "", "", "Kho Bac Lieu" },
                    { "BT-1", "https://maps.app.goo.gl/KQgjn4WQHpevbEwx7", "BT", "", "", "Kho Ben tre" },
                    { "CM-1", "https://maps.app.goo.gl/BAYMR65pPKQVuDFu9", "CM", "", "", "Kho Ca Mau" },
                    { "CT-1", "https://maps.app.goo.gl/JxNHUZ8eMcQatc5UA", "CT", "", "", "Kho Can Tho" },
                    { "DT-1", "https://maps.app.goo.gl/FVcUkiU8cwYTPNQj9", "DT", "", "", "Kho Dong Thap" },
                    { "HG-1", "https://maps.app.goo.gl/9MzoCy8ktS8YXDmn7", "HG", "", "", "Kho Hau Giang" },
                    { "KG-1", "https://maps.app.goo.gl/tfRFLY7Si1WrphLS7", "KG", "", "", "Kho Kien Giang" },
                    { "LA-1", "https://maps.app.goo.gl/f9ik6UNsUAUF2VEd6", "LA", "", "", "Kho Long An" },
                    { "ST-1", "https://maps.app.goo.gl/MVZvrKUtoEYpTcYb6", "ST", "", "", "Kho Soc Trang" },
                    { "TG-1", "https://maps.app.goo.gl/KSvnc2cwK613D1mW9", "TG", "", "", "Kho Tien Giang" },
                    { "TV-1", "https://maps.app.goo.gl/XcFvSjEzDDirLQFCA", "TV", "", "", "Kho Tra Vinh" },
                    { "VL-1", "https://maps.app.goo.gl/Jyaiqa9YL1zwBH7T8", "VL", "", "", "Kho Vinh Long" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Commune");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "EmployeePosition");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "PackageDetail");

            migrationBuilder.DropTable(
                name: "PackageLog");

            migrationBuilder.DropTable(
                name: "PackageWeight");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "ShipmentTrip");

            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "WHPath");

            migrationBuilder.DropTable(
                name: "WHRoute");
        }
    }
}
