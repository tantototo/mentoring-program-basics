
namespace XMLHandler.Models;

public class Catalog
{
    public long Id { get; set; }
    public DateTimeOffset? Date { get; set; }
    public string Library { get; set; }
    public IList<Book> Books { get; set; } = new List<Book>();
    public IList<Newspaper> Newspapers { get; set; } = new List<Newspaper>();
    public IList<Patent> Patents { get; set; } = new List<Patent>();
}