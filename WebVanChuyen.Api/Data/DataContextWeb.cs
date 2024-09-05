using Microsoft.EntityFrameworkCore;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.Api.Data
{
    public class DataContextWeb : DbContext
    {
        public DataContextWeb(DbContextOptions<DataContextWeb> options) : base(options) { }

        public DbSet<Province> Province { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Commune> Commune { get; set; }

        public DbSet<Warehouse> Warehouse { get; set; }

        public DbSet<WHPath> WHPath { get; set; }
        public DbSet<WHRoute> WHRoute { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeePosition> EmployeePosition { get; set; }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<Package> Package { get; set; }
        public DbSet<PackageWeight> PackageWeight { get; set; }
        public DbSet<PackageDetails> PackageDetail { get; set; }
        public DbSet<PackageLog> PackageLog { get; set; }

        public DbSet<ShipmentTrip> ShipmentTrip { get; set; }
        public DbSet<Truck> Truck { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed Province
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "AG", ProvinceName = "An Giang",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "BL", ProvinceName = "Bac Lieu",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "BT", ProvinceName = "Ben Tre",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "CM", ProvinceName = "Ca Mau",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "CT", ProvinceName = "Can Tho", });
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "DT", ProvinceName = "Dong Thap",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "HG", ProvinceName = "Hau Giang",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "KG", ProvinceName = "Kien Giang",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "LA", ProvinceName = "Long An",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "ST", ProvinceName = "Soc Trang",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "TG", ProvinceName = "Tien Giang",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "TV", ProvinceName = "Tra Vinh",});
            modelBuilder.Entity<Province>().HasData(new Province { ProvinceId = "VL", ProvinceName = "Vinh Long",});

            // AG - Tp. Long Xuyen
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 883, DistrictName = "Tp. Long Xuyen", ProvinceId = "AG" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30280, CommuneName = "Phuong My Binh", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30283, CommuneName = "Phuong My Long", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30285, CommuneName = "Phuong Dong Xuyen", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30286, CommuneName = "Phuong My Xuyen", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30289, CommuneName = "Phuong Binh Duc", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30292, CommuneName = "Phuong Binh Khanh", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30295, CommuneName = "Phuong My Phuoc", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30298, CommuneName = "Phuong My Quy", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30301, CommuneName = "Phuong My Thoi", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30304, CommuneName = "Phuong My Thạnh", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30307, CommuneName = "Phuong My Hoa", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30310, CommuneName = "Xa My Khanh", DistrictId = 883 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30313, CommuneName = "Xa My Hoa Hung", DistrictId = 883 });

            // BL - H. Vinh Loi
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 958, DistrictName = "H. Vinh Loi", ProvinceId = "BL" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31894, CommuneName = "Xa Vinh Hung", DistrictId = 958 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31897, CommuneName = "Xa Vinh Hung A", DistrictId = 958 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31900, CommuneName = "Thi tran Chau Hung", DistrictId = 958 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31903, CommuneName = "Xa Chau Hung A", DistrictId = 958 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31906, CommuneName = "Xa Hung Thanh", DistrictId = 958 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31909, CommuneName = "Xa Hung Hoi", DistrictId = 958 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31912, CommuneName = "Xa Chau Thoi", DistrictId = 958 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31921, CommuneName = "Xa Long Thanh", DistrictId = 958 });

            // BT - H. Chau Thanh
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 831, DistrictName = "H. Chau Thanh", ProvinceId = "BT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28801, CommuneName = "Thi tran Chau Thanh", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28804, CommuneName = "Xa Tan Thach", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28807, CommuneName = "Xa Quoi Son", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28810, CommuneName = "Xa An Khanh", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28813, CommuneName = "Xa Giao Long", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28819, CommuneName = "Xa Phu Tuc", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28822, CommuneName = "Xa Phu Đuc", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28825, CommuneName = "Xa Phu An Hoa", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28828, CommuneName = "Xa An Phuoc", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28831, CommuneName = "Xa Tam Phuoc", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28834, CommuneName = "Xa Thanh Trieu", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28837, CommuneName = "Xa Tuong Đa", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28840, CommuneName = "Xa Tan Phu", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28843, CommuneName = "Xa Quoi Thanh", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28846, CommuneName = "Xa Phuoc Thanh", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28849, CommuneName = "Xa An Hoa", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28852, CommuneName = "Xa Tien Long", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28855, CommuneName = "Xa An Hiep", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28858, CommuneName = "Xa Huu Đinh", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28861, CommuneName = "Thi tran Tien Thuy", DistrictId = 831 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28864, CommuneName = "Xa Son Hoa", DistrictId = 831 });

            // CM - Tp. Ca Mau
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 964, DistrictName = "Tp. Ca Mau", ProvinceId = "CM" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31999, CommuneName = "Phuong 9", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32002, CommuneName = "Phuong 4", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32005, CommuneName = "Phuong 1", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32008, CommuneName = "Phuong 5", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32011, CommuneName = "Phuong 2", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32014, CommuneName = "Phuong 8", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32017, CommuneName = "Phuong 6", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32020, CommuneName = "Phuong 7", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32022, CommuneName = "Phuong Tan Xuyen", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32023, CommuneName = "Xa An Xuyen", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32025, CommuneName = "Phuong Tan Thanh", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32026, CommuneName = "Xa Tan Thanh", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32029, CommuneName = "Xa Tac Van", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32032, CommuneName = "Xa Ly Van Lam", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32035, CommuneName = "Xa Dinh Binh", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32038, CommuneName = "Xa Hoa Thanh", DistrictId = 964 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 32041, CommuneName = "Xa Hoa Tan", DistrictId = 964 });

            // CT - Q. Ninh Kieu
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 916, DistrictName = "Q. Ninh Kieu", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31117, CommuneName = "Phuong Cai Khe", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31120, CommuneName = "Phuong An Hoa", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31123, CommuneName = "Phuong Thoi Binh", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31126, CommuneName = "Phuong An Nghiep", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31129, CommuneName = "Phuong An Cu", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31135, CommuneName = "Phuong Tan An", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31141, CommuneName = "Phuong An Phu", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31144, CommuneName = "Phuong Xuan Khanh", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31147, CommuneName = "Phuong Hung Loi", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31149, CommuneName = "Phuong An Khanh", DistrictId = 916 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31150, CommuneName = "Phuong An Binh", DistrictId = 916 });


            // CT - Q. Ninh Kieu
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 917, DistrictName = "Q. O Mon", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31153, CommuneName = "Phuong Chau Van Liem", DistrictId = 917 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31154, CommuneName = "Phuong Thoi Hoa", DistrictId = 917 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31156, CommuneName = "Phuong Thoi Long", DistrictId = 917 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31157, CommuneName = "Phuong Long Hung", DistrictId = 917 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31159, CommuneName = "Phuong Thoi An", DistrictId = 917 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31162, CommuneName = "Phuong Phuoc Thoi", DistrictId = 917 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31165, CommuneName = "Phuong Truong Lac", DistrictId = 917 });

            // CT - Q. Binh Thuy
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 918, DistrictName = "Q. Binh Thuy", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31168, CommuneName = "Phuong Binh Thuy", DistrictId = 918 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31169, CommuneName = "Phuong Tra An", DistrictId = 918 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31171, CommuneName = "Phuong Tra Noc", DistrictId = 918 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31174, CommuneName = "Phuong Thoi An Dong", DistrictId = 918 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31177, CommuneName = "Phuong An Thoi", DistrictId = 918 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31178, CommuneName = "Phuong Bui Huu Nghia", DistrictId = 918 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31180, CommuneName = "Phuong Long Hoa", DistrictId = 918 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31183, CommuneName = "Phuong Long Tuyen", DistrictId = 918 });

            // CT - Q. Cai Rang
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 919, DistrictName = "Q. Cai Rang", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31186, CommuneName = "Phuong Le Binh", DistrictId = 919 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31189, CommuneName = "Phuong Hung Phu", DistrictId = 919 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31192, CommuneName = "Phuong Hung Thanh", DistrictId = 919 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31195, CommuneName = "Phuong Ba Lang", DistrictId = 919 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31198, CommuneName = "Phuong Thuong Thanh", DistrictId = 919 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31201, CommuneName = "Phuong Phu Thu", DistrictId = 919 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31204, CommuneName = "Phuong Tan Phu", DistrictId = 919 });

            // CT - Q. Thot Not
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 923, DistrictName = "Q. Thot Not", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31207, CommuneName = "Phuong Thot Not", DistrictId = 923 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31210, CommuneName = "Phuong Thoi Thuan", DistrictId = 923 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31212, CommuneName = "Phuong Thuan An", DistrictId = 923 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31213, CommuneName = "Phuong Tan Loc", DistrictId = 923 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31216, CommuneName = "Phuong Trung Nhut", DistrictId = 923 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31217, CommuneName = "Phuong Thanh Hoa", DistrictId = 923 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31219, CommuneName = "Phuong Trung Kien", DistrictId = 923 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31227, CommuneName = "Phuong Tan Hung", DistrictId = 923 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31228, CommuneName = "Phuong Thuan Hung", DistrictId = 923 });

            // CT - H. Co Do
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 924, DistrictName = "H. Vinh Thanh", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31211, CommuneName = "Xa Vinh Binh", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31231, CommuneName = "Thi tran Thanh An", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31232, CommuneName = "Thi tran Vinh Thanh", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31234, CommuneName = "Xa Thanh My", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31237, CommuneName = "Xa Vinh Trinh", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31240, CommuneName = "Xa Thanh An", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31241, CommuneName = "Xa Thanh Tien", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31243, CommuneName = "Xa Thanh Thang", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31244, CommuneName = "Xa Thanh Loi", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31246, CommuneName = "Xa Thanh Quoi", DistrictId = 924 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31252, CommuneName = "Xa Thanh Loc", DistrictId = 924 });

            // CT - H. Vinh Thanh
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 925, DistrictName = "H. Co Do", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31222, CommuneName = "Xa Trung An", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31225, CommuneName = "Xa Trung Thanh", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31249, CommuneName = "Xa Thanh Phu", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31255, CommuneName = "Xa Trung Hung", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31261, CommuneName = "Thi tran Co Do", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31264, CommuneName = "Xa Thoi Hung", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31273, CommuneName = "Xa Dong Hiep", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31274, CommuneName = "Xa Dong Thang", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31276, CommuneName = "Xa Thoi Dong", DistrictId = 925 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31277, CommuneName = "Xa Thoi Xuân", DistrictId = 925 });

            // CT - H. Phong Dien
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 926, DistrictName = "H. Phong Dien", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31299, CommuneName = "Thi tran Phong Dien", DistrictId = 926 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31300, CommuneName = "Xa Nhon Ai", DistrictId = 926 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31303, CommuneName = "Xa Giai Xuan", DistrictId = 926 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31306, CommuneName = "Xa Tan Thoi", DistrictId = 926 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31309, CommuneName = "Xa Truong Long", DistrictId = 926 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31312, CommuneName = "Xa My Khanh", DistrictId = 926 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31315, CommuneName = "Xa Nhon Nghia", DistrictId = 926 });

            // CT - H. Thoi Lai
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 927, DistrictName = "H. Thoi Lai", ProvinceId = "CT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31258, CommuneName = "Thi tran Thoi Lai", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31267, CommuneName = "Xa Thoi Thanh", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31268, CommuneName = "Xa Tan Thanh", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31270, CommuneName = "Xa Xuan Thang", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31279, CommuneName = "Xa Dong Bình", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31282, CommuneName = "Xa Dong Thuan", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31285, CommuneName = "Xa Thoi Tan", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31286, CommuneName = "Xa Truong Thang", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31288, CommuneName = "Xa Dinh Mon", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31291, CommuneName = "Xa Truong Thanh", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31294, CommuneName = "Xa Truong Xuan", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31297, CommuneName = "Xa Truong Xuan A", DistrictId = 927 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31298, CommuneName = "Xa Truong Xuan B", DistrictId = 927 });


            // DT - Tp. Sa Dec
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 867, DistrictName = "Tp. Sa Dec", ProvinceId = "DT" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29902, CommuneName = "Phuong 3", DistrictId = 867 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29905, CommuneName = "Phuong 1", DistrictId = 867 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29908, CommuneName = "Phuong 4", DistrictId = 867 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29911, CommuneName = "Phuong 2", DistrictId = 867 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29914, CommuneName = "Xa Tan Khanh Dong", DistrictId = 867 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29917, CommuneName = "Phuong Tan Quy Dong", DistrictId = 867 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29919, CommuneName = "Phuong An Hoa", DistrictId = 867 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29920, CommuneName = "Xa Tan Quy Tay", DistrictId = 867 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29923, CommuneName = "Xa Tan Phu Dong", DistrictId = 867 });

            // HG - H. Vi Thuy
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 935, DistrictName = "H. Vi Thuy", ProvinceId = "HG" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31441, CommuneName = "Thi tran Nang Mau", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31444, CommuneName = "Xa Vi Trung", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31447, CommuneName = "Xa Vi Thuy", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31450, CommuneName = "Xa Vi Thang", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31453, CommuneName = "Xa Vinh Thuan Tay", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31456, CommuneName = "Xa Vinh Trung", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31459, CommuneName = "Xa Vinh Tuong", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31462, CommuneName = "Xa Vi Dong", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31465, CommuneName = "Xa Vi Thanh", DistrictId = 935 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31468, CommuneName = "Xa Vi Binh", DistrictId = 935 });

            // KG - H. Vi Thuy
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 899, DistrictName = "Tp. Rach Gia", ProvinceId = "KG" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30730, CommuneName = "Phuong Vinh Thanh Van", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30733, CommuneName = "Phuong Vinh Thanh", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30736, CommuneName = "Phuong Vinh Quang", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30739, CommuneName = "Phuong Vinh Hiep", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30742, CommuneName = "Phuong Vinh Bao", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30745, CommuneName = "Phuong Vinh Lac", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30748, CommuneName = "Phuong An Hoa", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30751, CommuneName = "Phuong An Binh", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30754, CommuneName = "Phuong Rach Soi", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30757, CommuneName = "Phuong Vinh Loi", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30760, CommuneName = "Phuong Vinh Thong", DistrictId = 899 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 30763, CommuneName = "Xa Phi Thong", DistrictId = 899 });

            // LA - H. Tan Thanh
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 799, DistrictName = "H. Tan Thanh", ProvinceId = "LA" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27826, CommuneName = "Thi tran Tan Thanh", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27829, CommuneName = "Xa Bac Hoa", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27832, CommuneName = "Xa Hau Thanh Tay", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27835, CommuneName = "Xa Nhon Hoa Lap", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27838, CommuneName = "Xa Tan Lap", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27841, CommuneName = "Xa Hau Thanh Dong", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27844, CommuneName = "Xa Nhon Hoa", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27847, CommuneName = "Xa Kien Binh", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27850, CommuneName = "Xa Tan Thanh", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27853, CommuneName = "Xa Tan Binh", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27856, CommuneName = "Xa Tan Ninh", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27859, CommuneName = "Xa Nhon Ninh", DistrictId = 799 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 27862, CommuneName = "Xa Tan Hoa", DistrictId = 799 });

            // ST - H. Long Phu
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 946, DistrictName = "H. Long Phu", ProvinceId = "ST" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31639, CommuneName = "Thi tran Long Phu", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31642, CommuneName = "Xa Song Phung", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31645, CommuneName = "Thi tran Dai Ngai", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31648, CommuneName = "Xa Hau Thanh", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31651, CommuneName = "Xa Long Duc", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31654, CommuneName = "Xa Truong Khanh", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31657, CommuneName = "Xa Phu Huu", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31660, CommuneName = "Xa Tan Hung", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31663, CommuneName = "Xa Chau Khanh", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31666, CommuneName = "Xa Tan Thanh", DistrictId = 946 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 31669, CommuneName = "Xa Long Phu", DistrictId = 946 });

            // TG - H. Chau Thanh
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 821, DistrictName = "H. Chau Thanh", ProvinceId = "TG" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28519, CommuneName = "Thi tran Tan Hiep", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28522, CommuneName = "Xa Tan Hoi Dong", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28525, CommuneName = "Xa Tan Huong", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28528, CommuneName = "Xa Tan Ly Dong", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28531, CommuneName = "Xa Tan Ly Tay", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28534, CommuneName = "Xa Than Cuu Nghia", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28537, CommuneName = "Xa Tam Hiep", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28540, CommuneName = "Xa Diem Hy", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28543, CommuneName = "Xa Nhi Binh", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28546, CommuneName = "Xa Duong Diem", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28549, CommuneName = "Xa Dong Hoa", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28552, CommuneName = "Xa Long Dinh", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28555, CommuneName = "Xa Huu Dao", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28558, CommuneName = "Xa Long An", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28561, CommuneName = "Xa Long Hung", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28564, CommuneName = "Xa Binh Trung", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28570, CommuneName = "Xa Thanh Phu", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28573, CommuneName = "Xa Ban Long", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28576, CommuneName = "Xa Vinh Kim", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28579, CommuneName = "Xa Binh Duc", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28582, CommuneName = "Xa Song Thuan", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28585, CommuneName = "Xa Kim Son", DistrictId = 821 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 28588, CommuneName = "Xa Phu Phong", DistrictId = 821 });

            // TV - Tp. Tra Vinh
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 842, DistrictName = "Tp. Tra Vinh", ProvinceId = "TV" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29236, CommuneName = "Phuong 4", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29239, CommuneName = "Phuong 1", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29242, CommuneName = "Phuong 3", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29245, CommuneName = "Phuong 2", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29248, CommuneName = "Phuong 5", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29251, CommuneName = "Phuong 6", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29254, CommuneName = "Phuong 7", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29257, CommuneName = "Phuong 8", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29260, CommuneName = "Phuong 9", DistrictId = 842 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29263, CommuneName = "Xa Long Đuc", DistrictId = 842 });

            // VL - Tp. Vinh Long
            modelBuilder.Entity<District>().HasData(new District { DistrictId = 855, DistrictName = "Tp. Vinh Long", ProvinceId = "VL" });
            // Seed Commune
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29542, CommuneName = "Phuong 9", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29545, CommuneName = "Phuong 5", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29548, CommuneName = "Phuong 1", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29551, CommuneName = "Phuong 2", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29554, CommuneName = "Phuong 4", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29557, CommuneName = "Phuong 3", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29560, CommuneName = "Phuong 8", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29563, CommuneName = "Phuong Tan Ngai", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29566, CommuneName = "Phuong Tan Hoa", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29569, CommuneName = "Phuong Tan Hoi", DistrictId = 855 });
            modelBuilder.Entity<Commune>().HasData(new Commune { CommuneId = 29572, CommuneName = "Phuong Truong An", DistrictId = 855 });

            // Seed Warehouses Table
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse 
            { 
                WarehouseId = "AG-1", 
                WarehouseName = "Kho An Giang", 
                ProvinceId = "AG",
                GPSCoordinates = "https://maps.app.goo.gl/uyH6tHXS8rkUWak16"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "BL-1",
                WarehouseName = "Kho Bac Lieu",
                ProvinceId = "BL",
                GPSCoordinates = "https://maps.app.goo.gl/BdPArUvT8Jbyk8dr9"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse 
            {
                WarehouseId = "BT-1",
                WarehouseName = "Kho Ben tre",
                ProvinceId = "BT",
                GPSCoordinates = "https://maps.app.goo.gl/KQgjn4WQHpevbEwx7"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse 
            { 
                WarehouseId = "CM-1",
                WarehouseName = "Kho Ca Mau",
                ProvinceId = "CM",
                GPSCoordinates = "https://maps.app.goo.gl/BAYMR65pPKQVuDFu9"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "CT-1",
                WarehouseName = "Kho Can Tho",
                ProvinceId = "CT",
                GPSCoordinates = "https://maps.app.goo.gl/JxNHUZ8eMcQatc5UA"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "DT-1",
                WarehouseName = "Kho Dong Thap",
                ProvinceId = "DT",
                GPSCoordinates = "https://maps.app.goo.gl/FVcUkiU8cwYTPNQj9"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "HG-1",
                WarehouseName = "Kho Hau Giang",
                ProvinceId = "HG",
                GPSCoordinates = "https://maps.app.goo.gl/9MzoCy8ktS8YXDmn7"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "KG-1",
                WarehouseName = "Kho Kien Giang",
                ProvinceId = "KG",
                GPSCoordinates = "https://maps.app.goo.gl/tfRFLY7Si1WrphLS7"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "LA-1",
                WarehouseName = "Kho Long An",
                ProvinceId = "LA",
                GPSCoordinates = "https://maps.app.goo.gl/f9ik6UNsUAUF2VEd6"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "ST-1",
                WarehouseName = "Kho Soc Trang",
                ProvinceId = "ST",
                GPSCoordinates = "https://maps.app.goo.gl/MVZvrKUtoEYpTcYb6"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "TG-1",
                WarehouseName = "Kho Tien Giang",
                ProvinceId = "TG",
                GPSCoordinates = "https://maps.app.goo.gl/KSvnc2cwK613D1mW9"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "TV-1",
                WarehouseName = "Kho Tra Vinh",
                ProvinceId = "TV",
                GPSCoordinates = "https://maps.app.goo.gl/XcFvSjEzDDirLQFCA"
            });
            modelBuilder.Entity<Warehouse>().HasData(new Warehouse
            {
                WarehouseId = "VL-1",
                WarehouseName = "Kho Vinh Long",
                ProvinceId = "VL",
                GPSCoordinates = "https://maps.app.goo.gl/Jyaiqa9YL1zwBH7T8"
            });

            // Seed EmployeePositions Table
            modelBuilder.Entity<EmployeePosition>().HasData(new EmployeePosition { PositionId = 1, PositionName = "Unspecified" });
            modelBuilder.Entity<EmployeePosition>().HasData(new EmployeePosition { PositionId = 2, PositionName = "Manager" });
            modelBuilder.Entity<EmployeePosition>().HasData(new EmployeePosition { PositionId = 3, PositionName = "Staff" });
            modelBuilder.Entity<EmployeePosition>().HasData(new EmployeePosition { PositionId = 4, PositionName = "Driver" });
            modelBuilder.Entity<EmployeePosition>().HasData(new EmployeePosition { PositionId = 5, PositionName = "Shipper" });

            // Seed Employee Table
            // Employee AG-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 4,
                EmployeeName = "Ngoc Sa",
                PositionId = 2,
                WWarehouseId = "AG-1",
                LocaId = "AG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 5,
                EmployeeName = "Bao Loc",
                PositionId = 3,
                WWarehouseId = "AG-1",
                LocaId = "AG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 6,
                EmployeeName = "Thien Loc",
                PositionId = 4,
                WWarehouseId = "AG-1",
                LocaId = "AG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 9,
                EmployeeName = "Bao Loc",
                PositionId = 4,
                WWarehouseId = "AG-1",
                LocaId = "AG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 56,
                EmployeeName = "Nghia Loc",
                PositionId = 5,
                WWarehouseId = "AG-1",
                LocaId = "AG-1",
            });

            // Employee BL-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 10,
                EmployeeName = "Ngoc Thanh",
                PositionId = 2,
                WWarehouseId = "BL-1",
                LocaId = "BL-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 11,
                EmployeeName = "Minh Long",
                PositionId = 3,
                WWarehouseId = "BL-1",
                LocaId = "BL-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 12,
                EmployeeName = "Be Ba",
                PositionId = 4,
                WWarehouseId = "BL-1",
                LocaId = "BL-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 13,
                EmployeeName = "Huu Phuoc",
                PositionId = 5,
                WWarehouseId = "BL-1",
                LocaId = "BL-1",
            });

            // Employee BT-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 14,
                EmployeeName = "Ngoc Trinh",
                PositionId = 2,
                WWarehouseId = "BT-1",
                LocaId = "BT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 15,
                EmployeeName = "Huu Nghia",
                PositionId = 3,
                WWarehouseId = "BT-1",
                LocaId = "BT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 16,
                EmployeeName = "Huynh Duc",
                PositionId = 4,
                WWarehouseId = "BT-1",
                LocaId = "BT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 17,
                EmployeeName = "Minh Thong",
                PositionId = 5,
                WWarehouseId = "BT-1",
                LocaId = "BT-1",
            });

            // Employee CM-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 18,
                EmployeeName = "Binh Phuoc",
                PositionId = 2,
                WWarehouseId = "CM-1",
                LocaId = "CM-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 19,
                EmployeeName = "Trung Tin",
                PositionId = 3,
                WWarehouseId = "CM-1",
                LocaId = "CM-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 20,
                EmployeeName = "Quoc Bao",
                PositionId = 4,
                WWarehouseId = "CM-1",
                LocaId = "CM-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 21,
                EmployeeName = "Minh Sang",
                PositionId = 5,
                WWarehouseId = "CM-1",
                LocaId = "CM-1",
            });

            // Warehouse CT-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Anh Dung",
                PositionId = 2,
                WWarehouseId = "CT-1",
                LocaId = "CT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Thanh Hang",
                PositionId = 3,
                WWarehouseId = "CT-1",
                LocaId = "CT-1",
            }); ;
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 7,
                EmployeeName = "Ngoc Bich",
                PositionId = 3,
                WWarehouseId = "CT-1",
                LocaId = "CT-1",
            }); ;
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Quoc Bao",
                PositionId = 4,
                WWarehouseId = "CT-1",
                LocaId = "CT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 8,
                EmployeeName = "Minh Triet",
                PositionId = 4,
                WWarehouseId = "CT-1",
                LocaId = "CT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 55,
                EmployeeName = "Vu Minh",
                PositionId = 5,
                WWarehouseId = "CT-1",
                LocaId = "CT-1",
            });


            // Employee DT-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 22,
                EmployeeName = "Bao Thy",
                PositionId = 2,
                WWarehouseId = "DT-1",
                LocaId = "DT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 23,
                EmployeeName = "Minh Anh",
                PositionId = 3,
                WWarehouseId = "DT-1",
                LocaId = "DT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 24,
                EmployeeName = "Trung Thang",
                PositionId = 4,
                WWarehouseId = "DT-1",
                LocaId = "DT-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 25,
                EmployeeName = "Thien Phuoc",
                PositionId = 5,
                WWarehouseId = "DT-1",
                LocaId = "DT-1",
            });

            // Employee HG-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 26,
                EmployeeName = "Ngoc Bao",
                PositionId = 2,
                WWarehouseId = "HG-1",
                LocaId = "HG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 27,
                EmployeeName = "Lan Anh",
                PositionId = 3,
                WWarehouseId = "HG-1",
                LocaId = "HG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 28,
                EmployeeName = "Hoai Bao",
                PositionId = 4,
                WWarehouseId = "HG-1",
                LocaId = "HG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 29,
                EmployeeName = "Quoc Nghia",
                PositionId = 4,
                WWarehouseId = "HG-1",
                LocaId = "HG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 54,
                EmployeeName = "Bao Minh",
                PositionId = 5,
                WWarehouseId = "HG-1",
                LocaId = "HG-1",
            });

            // Employee KG-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 30,
                EmployeeName = "Thu Thuy",
                PositionId = 2,
                WWarehouseId = "KG-1",
                LocaId = "KG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 31,
                EmployeeName = "Bao An",
                PositionId = 3,
                WWarehouseId = "KG-1",
                LocaId = "KG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 32,
                EmployeeName = "Van Duc",
                PositionId = 4,
                WWarehouseId = "KG-1",
                LocaId = "KG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 33,
                EmployeeName = "Van Trong",
                PositionId = 5,
                WWarehouseId = "KG-1",
                LocaId = "KG-1",
            });

            // Employee LA-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 34,
                EmployeeName = "Thu Ngoc",
                PositionId = 2,
                WWarehouseId = "LA-1",
                LocaId = "LA-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 35,
                EmployeeName = "Bao Binh",
                PositionId = 3,
                WWarehouseId = "LA-1",
                LocaId = "LA-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 36,
                EmployeeName = "Van Binh",
                PositionId = 4,
                WWarehouseId = "LA-1",
                LocaId = "LA-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 37,
                EmployeeName = "Van Minh",
                PositionId = 5,
                WWarehouseId = "LA-1",
                LocaId = "LA-1",
            });

            // Employee ST-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 38,
                EmployeeName = "Minh Duc",
                PositionId = 2,
                WWarehouseId = "ST-1",
                LocaId = "ST-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 39,
                EmployeeName = "Hanh Ngoc",
                PositionId = 3,
                WWarehouseId = "ST-1",
                LocaId = "ST-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 40,
                EmployeeName = "Binh Minh",
                PositionId = 4,
                WWarehouseId = "ST-1",
                LocaId = "ST-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee    
            {
                EmployeeId = 41,
                EmployeeName = "Hoai Thu",
                PositionId = 5,
                WWarehouseId = "ST-1",
                LocaId = "ST-1",
            });

            // Employee TG-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 42,
                EmployeeName = "Duc Tin",
                PositionId = 2,
                WWarehouseId = "TG-1",
                LocaId = "TG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 43,
                EmployeeName = "Ngoc Hoa",
                PositionId = 3,
                WWarehouseId = "TG-1",
                LocaId = "TG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 44,
                EmployeeName = "Minh Trong",
                PositionId = 4,
                WWarehouseId = "TG-1",
                LocaId = "TG-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 45,
                EmployeeName = "Van Dung",
                PositionId = 5,
                WWarehouseId = "TG-1",
                LocaId = "TG-1",
            });

            // Employee TV-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 46,
                EmployeeName = "Bao Long",
                PositionId = 2,
                WWarehouseId = "TV-1",
                LocaId = "TV-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 47,
                EmployeeName = "Hoang Kim",
                PositionId = 3,
                WWarehouseId = "TV-1",
                LocaId = "TV-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 48,
                EmployeeName = "Minh An",
                PositionId = 4,
                WWarehouseId = "TV-1",
                LocaId = "TV-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 49,
                EmployeeName = "Quoc Huy",
                PositionId = 5,
                WWarehouseId = "TV-1",
                LocaId = "TV-1",
            });

            // Employee VL-1
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 50,
                EmployeeName = "Long Duc",
                PositionId = 2,
                WWarehouseId = "VL-1",
                LocaId = "VL-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 51,
                EmployeeName = "Kim Ngoc",
                PositionId = 3,
                WWarehouseId = "VL-1",
                LocaId = "VL-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 52,
                EmployeeName = "Hoang Duc",
                PositionId = 4,
                WWarehouseId = "VL-1",
                LocaId = "VL-1",
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 53,
                EmployeeName = "Quoc Hai",
                PositionId = 5,
                WWarehouseId = "VL-1",
                LocaId = "VL-1",
            });

            // Seed Category
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Thuc an" });
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 2, CategoryName = "Thuc uong" });
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 3, CategoryName = "De vo" });
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 4, CategoryName = "Dong lanh" });

            // Seed Product
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Gao" });
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 2, ProductName = "Thit heo dong lanh" });
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 3, ProductName = "Bia chai thuy tinh" });
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 4, ProductName = "Bia lon" });

            // Seed ProductCategories
            // Rice
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Product = 1, ProductCateId = 1, Category = 1 });

            // Freeze Pork
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductCateId = 2, Product = 2, Category = 1 });
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductCateId = 3, Product = 2, Category = 4 });

            // Glass Beer
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductCateId = 4, Product = 3, Category = 2 });
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductCateId = 5, Product = 3, Category = 3 });

            // Can Beer
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductCateId = 6, Product = 4, Category = 2 });

            // Seed Path between Warehouse
            // AG-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath 
            {
                PathId = 1,
                StartPoint = "AG-1",
                EndPoint = "CT-1",
                Cost = 63.95,
                ShipCost = 31975
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 2,
                StartPoint = "AG-1",
                EndPoint = "DT-1",
                Cost = 45.1,
                ShipCost = 22550
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 3,
                StartPoint = "AG-1",
                EndPoint = "KG-1",
                Cost = 75,
                ShipCost = 37500
            });

            // BL-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 4,
                StartPoint = "BL-1",
                EndPoint = "CM-1",
                Cost = 75.95,
                ShipCost = 37975
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 5,
                StartPoint = "BL-1",
                EndPoint = "HG-1",
                Cost = 64.25,
                ShipCost = 32125
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 6,
                StartPoint = "BL-1",
                EndPoint = "KG-1",
                Cost = 129,
                ShipCost = 64500
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 7,
                StartPoint = "BL-1",
                EndPoint = "ST-1",
                Cost = 46.05,
                ShipCost = 23025
            });

            // BT-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 8,
                StartPoint = "BT-1",
                EndPoint = "TG-1",
                Cost = 15.95,
                ShipCost = 7975
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 9,
                StartPoint = "BT-1",
                EndPoint = "TV-1",
                Cost = 48.35,
                ShipCost = 24175
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 10,
                StartPoint = "BT-1",
                EndPoint = "VL-1",
                Cost = 75.2,
                ShipCost = 37600
            });

            // CM-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 11,
                StartPoint = "CM-1",
                EndPoint = "KG-1",
                Cost = 121.5,
                ShipCost = 60750
            });

            // CT-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 12,
                StartPoint = "CT-1",
                EndPoint = "DT-1",
                Cost = 51.9,
                ShipCost = 25950
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 13,
                StartPoint = "CT-1",
                EndPoint = "HG-1",
                Cost = 43.45,
                ShipCost = 21725
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 14,
                StartPoint = "CT-1",
                EndPoint = "KG-1",
                Cost = 109.5,
                ShipCost = 54750
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 15,
                StartPoint = "CT-1",
                EndPoint = "VL-1",
                Cost = 38.15,
                ShipCost = 19075
            });

            // DT-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 16,
                StartPoint = "DT-1",
                EndPoint = "LA-1",
                Cost = 87.35,
                ShipCost = 43675
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 17,
                StartPoint = "DT-1",
                EndPoint = "TG-1",
                Cost = 85.05,
                ShipCost = 42525
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 18,
                StartPoint = "DT-1",
                EndPoint = "VL-1",
                Cost = 31.35,
                ShipCost = 15675
            });

            // HG-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 19,
                StartPoint = "HG-1",
                EndPoint = "KG-1",
                Cost = 66.5,
                ShipCost = 33250
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 20,
                StartPoint = "HG-1",
                EndPoint = "ST-1",
                Cost = 81.2,
                ShipCost = 40600
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 21,
                StartPoint = "HG-1",
                EndPoint = "VL-1",
                Cost = 79.5,
                ShipCost = 39750
            });

            // LA-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 22,
                StartPoint = "LA-1",
                EndPoint = "TG-1",
                Cost = 51.65,
                ShipCost = 25825
            });

            // ST-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 23,
                StartPoint = "ST-1",
                EndPoint = "TV-1",
                Cost = 57.7,
                ShipCost = 28850
            });
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 24,
                StartPoint = "ST-1",
                EndPoint = "VL-1",
                Cost = 97.9,
                ShipCost = 48950
            });

            // TG-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 25,
                StartPoint = "TG-1",
                EndPoint = "VL-1",
                Cost = 77.9,
                ShipCost = 38950
            });

            // TV-1
            modelBuilder.Entity<WHPath>().HasData(new WHPath
            {
                PathId = 26,
                StartPoint = "TV-1",
                EndPoint = "VL-1",
                Cost = 60.4,
                ShipCost = 30200
            });

            // Seed PackageWeight
            modelBuilder.Entity<PackageWeight>().HasData(new PackageWeight { Weight = 0.5, WCost = 0 });
            modelBuilder.Entity<PackageWeight>().HasData(new PackageWeight { Weight = 1.0, WCost = 1500 });
            modelBuilder.Entity<PackageWeight>().HasData(new PackageWeight { Weight = 1.5, WCost = 3000 });
            modelBuilder.Entity<PackageWeight>().HasData(new PackageWeight { Weight = 2.0, WCost = 4500 });
            modelBuilder.Entity<PackageWeight>().HasData(new PackageWeight { Weight = 2.5, WCost = 5500 });
            modelBuilder.Entity<PackageWeight>().HasData(new PackageWeight { Weight = 3.0, WCost = 6500 });

            // Seed Truck
            // AG-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "67-01", LocaId = "AG-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "67-02", LocaId = "AG-1" });

            // BL-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "94-1", LocaId = "BL-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "94-2", LocaId = "BL-1" });

            // BT-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "71-1", LocaId = "BT-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "71-2", LocaId = "BT-1" });

            // CM-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "69-1", LocaId = "CM-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "69-2", LocaId = "CM-1" });

            // CT-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "65-01", LocaId = "CT-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "65-02", LocaId = "CT-1" });

            // DT-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "66-1", LocaId = "DT-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "66-2", LocaId = "DT-1" });

            // HG-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "95-1", LocaId = "HG-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "95-2", LocaId = "HG-1" });

            // KG-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "68-1", LocaId = "KG-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "68-2", LocaId = "KG-1" });

            // LA-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "62-1", LocaId = "LA-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "62-2", LocaId = "LA-1" });

            // ST-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "83-1", LocaId = "ST-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "83-2", LocaId = "ST-1" });

            // TG-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "63-1", LocaId = "TG-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "63-2", LocaId = "TG-1" });

            // TV-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "84-1", LocaId = "TG-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "84-2", LocaId = "TG-1" });

            // VL-1
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "64-1", LocaId = "VL-1" });
            modelBuilder.Entity<Truck>().HasData(new Truck { TruckId = "64-2", LocaId = "VL-1" });

        }
    }
}
