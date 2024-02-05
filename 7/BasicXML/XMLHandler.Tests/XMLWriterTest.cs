using XMLHandler.Models;

namespace XMLHandler.Tests;

public class XMLWriterTest
{
    [Fact]
    public void WriteXML_Success()
    {
        // Arrange
        var path = @"...\test.xml";
        
        var book = new Book()
        {
            Id = 1,
            Name = "Sapiens",
            Author = "Yuval Noah Harari",
            Location = "Israel",
            Publisher = "Dvir Publishing House Ltd.",
            Year = 2011
        };
        var book2 = new Book()
        {
            Id = 15,
            Name = "It",
            Author = "King Stephen",
            Publisher = "Hodder",
            PageCount = 1392,
            ISBN = "9781444707861"
        };
        var newspaper = new Newspaper()
        {
            Id = 1,
            Name = "Ornament",
            Publisher = "Ornament Mag",
            Year = 2023,
            ISSN = "19966"
        };
            
        var catalog = new Catalog()
        {
            Id = 1,
            Date = DateTimeOffset.Now,
            Library = "Trinity college library",
            Books = new List<Book>() {book, book2},
            Newspapers = new List<Newspaper>() {newspaper}
        };

        // Act 
        XMLWriter.WriteXml(catalog, path);
        
        // Assert 
        Assert.True(File.Exists(path));
    }
    
    [Fact]
    public void WriteXML_Failure()
    {
        // Arrange
        var path = @"...\test.xml";
        var catalog = new Catalog()
        {
            Library = "Trinity college library",
            Books = new List<Book>(),
            Newspapers = new List<Newspaper>() {new()}
        };

        // Assert
        Assert.Throws<Exception>(() => XMLWriter.WriteXml(catalog, path));
    }
    
    [Fact]
    public void WriteXmlElement_Success()
    {
        // Arrange
        var path = @"...\book.xml";
        var book = new Book()
        {
            Id = 1,
            Name = "Sapiens",
            Author = "Yuval Noah Harari",
            Location = "Israel",
            Publisher = "Dvir Publishing House Ltd.",
            Year = 2011
        };

        // Act 
        XMLWriter.WriteXmlElement(path, book);
        
        // Assert 
        Assert.True(File.Exists(path));
    }
}