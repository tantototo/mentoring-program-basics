namespace BookLibrary.Models
{
    public class Book
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public long Rack { get; set; }
        public long Shelf { get; set; }
        public string? Description { get; set; }
        public long? PageCount { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? Sity { get; set; }
        public long? Year { get; set; }
        public string? ISBN { get; set; }
        public Category? Category { get; set; }
    }
}
