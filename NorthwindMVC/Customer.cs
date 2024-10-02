using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindMVC
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public string CustomerId { get; set; }
        [Column("company_name")]
        public string CompanyName { get; set; }
        [Column("contact_name")]
        public string ContactName { get; set; }
        [Column("country")]
        public string Country { get; set; }

    }
}
