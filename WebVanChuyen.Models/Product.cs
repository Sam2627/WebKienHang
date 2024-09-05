using System.ComponentModel.DataAnnotations;

namespace WebVanChuyen.Models.Models
{
    public class Product
    {
        [Key] public int ProductId { get; set; }

        [Required(ErrorMessage = "Sản phẩm phải có tên")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Tên sản phẩm tối thiểu có 1 - 100 kí tự")]
        public string ProductName { get; set; }

        public string ProductDesc { get; set; } = string.Empty;
    }

    public class Category
    {
        [Key] public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class ProductCategory
    {
        [Key] public int ProductCateId { get; set; }
        public int Product { get; set; }
        public int Category { get; set; }
    }

    public class AddCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsCheck { get; set; } = false;
    }
}
