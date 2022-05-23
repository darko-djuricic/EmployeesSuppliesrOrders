using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Drugi_projekat.models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int ProductId { get; set; }
        [Display(Name ="Product name")]
        public string ProductName { get; set; } = null!;
        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        [Display(Name = "Quantity per unit")]
        public string? QuantityPerUnit { get; set; }
        [Display(Name = "Unit price")]
        [DataType(DataType.Currency)]
        public decimal? UnitPrice { get; set; }
        [Display(Name = "Units in stock")]
        public short? UnitsInStock { get; set; }
        [Display(Name = "Units on order")]
        public short? UnitsOnOrder { get; set; }
        [Display(Name = "Reorder level")]
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
