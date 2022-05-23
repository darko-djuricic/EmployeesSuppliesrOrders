using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Drugi_projekat.models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [DisplayName("Order ID")]
        public int OrderId { get; set; }
        [DisplayName("Customer ID")]
        public string? CustomerId { get; set; }
        [DisplayName("Employee ID")]
        public int? EmployeeId { get; set; }
        [DisplayName("Order date")]
        public DateTime? OrderDate { get; set; }
        [DisplayName("Required date")]
        public DateTime? RequiredDate { get; set; }
        [DisplayName("Shipped date")]
        public DateTime? ShippedDate { get; set; }
        [DisplayName("Ship via")]
        public int? ShipVia { get; set; }
        [DisplayName("Freight")]
        public decimal? Freight { get; set; }
        [DisplayName("Ship name")]
        public string? ShipName { get; set; }
        [DisplayName("Ship address")]
        public string? ShipAddress { get; set; }
        [DisplayName("Ship city")]
        public string? ShipCity { get; set; }
        [DisplayName("Ship region")]
        public string? ShipRegion { get; set; }
        [DisplayName("Ship postal code")]
        public string? ShipPostalCode { get; set; }
        [DisplayName("Ship country")]
        public string? ShipCountry { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Shipper? ShipViaNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
