namespace EF.Infra.Entities
{
    public class Customer
    {
        public required string CustomerId { get; set; }
        public required string CompanyName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
