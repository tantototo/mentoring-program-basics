using System.Xml;

namespace XMLHandler.Tests;

public class XMLReaderTest
{
    [Fact]
    public void ReadXML_Success()
    {
        // Arrange
        var path = @"...\test.xml";

        // Act 
        var result = XMLReader.ReadXml(path);
        
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public void ReadXML_Failure()
    {
        // Arrange
        var path = @"";

        // Assert
        Assert.Throws<Exception>(() => XMLReader.ReadXml(path));
    }

}