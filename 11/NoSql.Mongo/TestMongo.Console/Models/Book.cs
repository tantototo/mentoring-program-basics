namespace TestMongo.Console.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }
        public IList<string> Genres { get; set; } = new List<string>();
        public int Year { get; set; }
    }
}
