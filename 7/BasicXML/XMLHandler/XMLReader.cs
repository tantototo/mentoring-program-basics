using System.Data;
using System.Xml;
using XMLHandler.Models;

namespace XMLHandler;

public class XMLReader
{
    public static Catalog ReadXml(string path)
    {
        if(!File.Exists(path))
            throw new Exception("Incorrect path");
        
        var doc = new XmlDocument();
        doc.Load(path);

        var elements = doc.DocumentElement;
        if (elements is not { Name: "catalog" })
            throw new Exception("Incorrect file");
        
        var catalog = new Catalog();
        foreach (XmlElement xml in elements)
        {
            switch (xml.Name)
            {
                case "id":
                    try { catalog.Id = long.Parse(xml.InnerText); }
                    catch { throw new NoNullAllowedException("Catalog id is required."); }
                    break;
                case "library":
                    catalog.Library = xml.InnerText;
                    break;
                case "date":
                    try { catalog.Date = DateTimeOffset.Parse(xml.InnerText); } catch { }
                    break;
                case "books":
                    catalog.Books = GetBooks(xml.ChildNodes);
                    break;
                case "newspapers":
                    catalog.Newspapers = GetNewspapers(xml.ChildNodes);
                    break;
                case "patents":
                    catalog.Patents = GetPatents(xml.ChildNodes);
                    break;
            }
        }

        return catalog;
    }

    static T PreparePolygraphy<T>(Polygraphy poly, XmlElement element) where T : Polygraphy
    {
        switch (element.Name)
        {
            case "id":
                try { poly.Id = long.Parse(element.InnerText); }
                catch { throw new NoNullAllowedException("Element id is required."); }
                break;
            case "name":
                poly.Name = element.InnerText;
                if (string.IsNullOrEmpty(poly.Name))
                    throw new NoNullAllowedException("Element name is required.");
                break;
            case "location":
                poly.Location = element.InnerText;
                break;
            case "pageCount":
                try { poly.PageCount = long.Parse(element.InnerText); } catch { }
                break;
            case "note":
                poly.Note = element.InnerText;
                break;
        }
        return (T)poly;
    }

    static IEnumerable<Book> GetBooks(XmlNodeList listBooks)
    {
        var books = new List<Book>();
        foreach (XmlElement bookXml in listBooks)
        {
            var book = new Book();
            foreach (XmlElement element in bookXml)
            {
                switch (element.Name)
                {
                    case "author":
                        book.Author = element.InnerText;
                        break;
                    case "publisher":
                        book.Publisher = element.InnerText;
                        break;
                    case "year":
                        try { book.Year = long.Parse(element.InnerText); } catch { }
                        break;
                    case "isbn":
                        book.ISBN = element.InnerText;
                        break;
                    default: PreparePolygraphy<Book>(book, element);
                        break;
                }
            }
            books.Add(book);
        }
        return books;
    }
    
    static IEnumerable<Newspaper> GetNewspapers(XmlNodeList listNewspapers)
    {
        var newspapers = new List<Newspaper>();
        foreach (XmlElement newspaperXml in listNewspapers)
        {
            var newspaper = new Newspaper();
            foreach (XmlElement element in newspaperXml)
            {
                switch (element.Name)
                {
                    case "publisher":
                        newspaper.Publisher = element.InnerText;
                        break;
                    case "year":
                        try { newspaper.Year = long.Parse(element.InnerText); } catch { }
                        break;
                    case "date":
                        try { newspaper.Date = DateOnly.Parse(element.InnerText); } catch { }
                        break;
                    case "issn":
                        newspaper.ISSN = element.InnerText;
                        break;
                    default: PreparePolygraphy<Newspaper>(newspaper, element);
                        break;
                }
            }
            newspapers.Add(newspaper);
        }
        return newspapers;
    }
    
    static IEnumerable<Patent> GetPatents(XmlNodeList listPatents)
    {
        var patents = new List<Patent>();
        foreach (XmlElement patentXml in listPatents)
        {
            var patent = new Patent();
            foreach (XmlElement element in patentXml)
            {
                switch (element.Name)
                {
                    case "author":
                        patent.Author = element.InnerText;
                        break;
                    case "publicationDate":
                        try { patent.PublicationDate = DateOnly.Parse(element.InnerText); } catch { }
                        break;
                    case "requestDate":
                        try { patent.RequestDate = DateOnly.Parse(element.InnerText); } catch { }
                        break;
                    case "registrationNumber":
                        patent.RegistrationNumber = element.InnerText;
                        break;
                    default: PreparePolygraphy<Patent>(patent, element);
                        break;
                }
            }
            patents.Add(patent);
        }
        return patents;
    }
    
}