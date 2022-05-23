using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Drugi_projekat.models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseReportsToNavigation = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            Territories = new HashSet<Territory>();
        }
        [Key]
        public int EmployeeId { get; set; }
        [StringLength(20)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = null!;
        [StringLength(20)]
        [DisplayName("First name")]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string? Title { get; set; }
        [DisplayName("Title of courtesy")]
        public string? TitleOfCourtesy { get; set; }
        [DisplayName("Home phone")]
        [RegularExpression(@"((\(\d{2,3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid phone number")]
        public string? HomePhone { get; set; }
        [DisplayName("Birth date")]
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        [DisplayName("Postal code")]
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        [DisplayName("Hire date")]
        public DateTime? HireDate { get; set; }
        public string? Extension { get; set; }
        public byte[]? Photo { get; set; }
        public string? Notes { get; set; }
        [DisplayName("Reports to")]
        public int? ReportsTo { get; set; }
        [DisplayName("Photo path")]
        public string? PhotoPath { get; set; }
        [DisplayName("Reports to navigation")]
        public virtual Employee? ReportsToNavigation { get; set; }
        [DisplayName("Inverse reports to navigation")]
        public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Territory> Territories { get; set; }
    }
}
