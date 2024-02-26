namespace EF.Infra.Entities
{
    public class CreditCard
    {
        public int CardNumber { get; set; }
        public int EmployeeId { get; set; }
        public required string CardHolder { get; set; }
        public DateTime ExpireDate { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
