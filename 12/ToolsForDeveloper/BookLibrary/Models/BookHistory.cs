namespace BookLibrary.Models
{
    public class BookHistory
    {
        public required Book Book { get; set; }
        public required Visitor Visitor { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EstimatedEndDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
