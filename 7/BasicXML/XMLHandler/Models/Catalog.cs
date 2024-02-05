
namespace XMLHandler.Models;

public class Catalog
{
    public long Id { get; set; }
    public DateTimeOffset? Date { get; set; }
    public string Library { get; set; }
    
    public IEnumerable<Book> Books
    {
        set => books = value;
        get => books ?? Enumerable.Empty<Book>();
    }
    public IEnumerable<Newspaper> Newspapers
    {
        set => newspapers = value;
        get => newspapers ?? Enumerable.Empty<Newspaper>();
    }

    public IEnumerable<Patent> Patents
    {
        set => patents = value;
        get => patents ?? Enumerable.Empty<Patent>();
    }
    
    private IEnumerable<Book> books;
    private IEnumerable<Newspaper> newspapers;
    private IEnumerable<Patent> patents;
}