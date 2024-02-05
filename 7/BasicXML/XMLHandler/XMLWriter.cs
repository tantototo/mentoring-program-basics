using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMLHandler.Models;

namespace XMLHandler;

public class XMLWriter
{
    public static void WriteXml(Catalog catalog, string path)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        if (catalog.Id == 0)
            throw new Exception("Incorrect catalog Id.");
        
        var catalogXml =
            new XElement("catalog",
                new XElement("id", catalog.Id),
                new XElement("library", catalog.Library),
                new XElement("date", catalog.Date)
            );

        var books = new XElement("books");
        foreach (var element in catalog.Books)
        {
            var book =
                new XElement("book",
                    new XElement("id", element.Id),
                    new XElement("name", element.Name),
                    new XElement("author", element.Author),
                    new XElement("publisher", element.Publisher),
                    new XElement("location", element.Location),
                    new XElement("year", element.Year),
                    new XElement("pageCount", element.PageCount),
                    new XElement("note", element.Note),
                    new XElement("isbn", element.ISBN)
                );
            books.Add(book);
        }
        catalogXml.Add(books);
        
        var newspapers = new XElement("newspapers");
        foreach (var element in catalog.Newspapers)
        {
            var newspaper =
                new XElement("newspaper",
                    new XElement("id", element.Id),
                    new XElement("name", element.Name),
                    new XElement("date", element.Date),
                    new XElement("publisher", element.Publisher),
                    new XElement("location", element.Location),
                    new XElement("year", element.Year),
                    new XElement("pageCount", element.PageCount),
                    new XElement("note", element.Note),
                    new XElement("issn", element.ISSN)
                );
            newspapers.Add(newspaper);
        }
        catalogXml.Add(newspapers);
        
        var patents = new XElement("patents");
        foreach (var element in catalog.Patents)
        {
            var patent =
                new XElement("patent",
                    new XElement("id", element.Id),
                    new XElement("name", element.Name),
                    new XElement("author", element.Author),
                    new XElement("location", element.Location),
                    new XElement("publicationDate", element.PublicationDate),
                    new XElement("requestDate", element.RequestDate),
                    new XElement("pageCount", element.PageCount),
                    new XElement("note", element.Note),
                    new XElement("registrationNumber", element.RegistrationNumber)
                );
            patents.Add(patent);
        }
        catalogXml.Add(patents);
        
        var writer = new StreamWriter(path);
        //var writer = new StringWriter();
        var xmlWriter = XmlWriter.Create(writer,
            new XmlWriterSettings { Indent = true });

        try
        {
            catalogXml.WriteTo(xmlWriter);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            xmlWriter.Close();
            writer.Close();
        }

        Console.WriteLine(writer);
    }
    
    public static void WriteXmlElement(string path, object value)
    {
        var writer = new StreamWriter(path);
        try
        {
            var serializer = new XmlSerializer(value.GetType());
            serializer.Serialize(writer, value);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            writer.Close();
        }
    }
}