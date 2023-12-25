namespace SystemVisitor.Tests;

public class SystemVisitorTest
{
    [Fact]
    public void CheckWithoutFilter_Success()
    {
        // Arrange
        var root = @"";

        // Act 
        var visitor = new FileSystemVisitor(root);

        // Assert 
        int expectCount = 7;
        Assert.Equal(expectCount, visitor.Count());
    }
    
    [Fact]
    public void CheckWithFileFilter_Success()
    {
        // Arrange
        var root = @"";
        var filter = "";
        Predicate<string> predicate = x => x.Contains(filter);

        // Act 
        var visitor = new FileSystemVisitor(root, predicate).ToArray();

        // Assert 
        int expectCount = 5;
        Assert.Equal(expectCount, visitor.Length);
        for (int i = 0; i < visitor.Length; i++) 
            Assert.Contains(filter, visitor[i].Name);
    }
    
    [Fact]
    public void CheckWithObjectNameFilter_Success()
    {
        // Arrange
        var root = @"";
        var filter = "";
        Predicate<string> predicate = x => x.Contains(filter);

        // Act 
        var visitor = new FileSystemVisitor(root, predicate).ToArray();

        // Assert 
        int expectCount = 10;
        Assert.Equal(expectCount, visitor.Length);
        for (int i = 0; i < visitor.Length; i++) 
            Assert.Contains(filter, visitor[i].FullName);
    }
    
    [Fact]
    public void CheckEvent_StopAfterFilterFound_Success()
    {
        // Arrange
        var root = @"";
        var filter = "";
        Predicate<string> predicate = x => x.Contains(filter);

        // Act 
        var visitor = new FileSystemVisitor(root, predicate);
        visitor.EventHelper.FilteredFileFinded += (sender, args) => { args.Cancel = true;};
        visitor.EventHelper.FilteredDirectoryFinded += (sender, args) => { args.Cancel = true;};
        var visitorArray = visitor.ToArray();

        // Assert 
        int expectCount = 1;
        Assert.Equal(expectCount, visitorArray.Length);
        for (int i = 0; i < visitorArray.Length; i++) 
            Assert.Contains(filter, visitorArray[i].FullName);
    }
    
    [Fact]
    public void CheckEvent_ExcludeWithFilter_Success()
    {
        // Arrange
        var root = @"";
        var filter = "";
        Predicate<string> predicate = x => x.Contains(filter);

        // Act 
        var visitor = new FileSystemVisitor(root, predicate);
        visitor.EventHelper.FileFinded += (sender, args) => { args.Exclude = true;};
        visitor.EventHelper.FilteredFileFinded += (sender, args) => { args.Exclude = true;};
        visitor.EventHelper.FilteredDirectoryFinded += (sender, args) => { args.Exclude = true;};
        var visitorArray = visitor.ToArray();

        // Assert 
        int expectCount = 7;
        Assert.Equal(expectCount, visitorArray.Length);
        for (int i = 0; i < visitorArray.Length; i++) 
            Assert.DoesNotContain(filter, visitorArray[i].FullName);
    }
}