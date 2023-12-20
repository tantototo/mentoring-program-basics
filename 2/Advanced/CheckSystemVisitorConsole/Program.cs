using SystemVisitor;

var path = string.Empty;
while (string.IsNullOrEmpty(path))
{
    Console.Write("Input path: ");
    path = Console.ReadLine();
}
Console.Write("Input filter: ");
var filter = Console.ReadLine();

FileSystemVisitor visitor;
if (string.IsNullOrEmpty(filter))
{
    visitor = new FileSystemVisitor(path);
}
else
{
    Predicate<string> predicate = x => x.Contains(filter);
    visitor = new FileSystemVisitor(path, predicate);
}

//visitor.EventHelper.FileFinded += ManageEvent;
visitor.EventHelper.FilteredFileFinded += ManageEvent;
visitor.EventHelper.FilteredDirectoryFinded += ManageEvent;

var result = new List<FileSystemInfo>();
foreach (var item in visitor)
{
    result.Add(item);
}

Console.WriteLine("Result:");
foreach (var item in result)
{
    Console.WriteLine(item);
}

void ManageEvent(object sender, SearchEventArgs e)
{
    //e.Cancel = true;
    //e.Exclude = true;
    Console.WriteLine(e.Message);
}