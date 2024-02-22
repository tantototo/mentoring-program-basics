
namespace XMLHandler.Models;

public class Catalog
{
    public long Id { get; set; }
    public DateTimeOffset? Date { get; set; }
    public string Library { get; set; }
    public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
    public IEnumerable<Newspaper> Newspapers { get; set; } = Enumerable.Empty<Newspaper>();
    public IEnumerable<Patent> Patents { get; set; } = Enumerable.Empty<Patent>();
}