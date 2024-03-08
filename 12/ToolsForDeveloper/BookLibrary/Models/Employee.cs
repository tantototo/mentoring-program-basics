namespace BookLibrary.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
    }
}
