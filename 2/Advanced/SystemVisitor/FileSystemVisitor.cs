using System.Collections;

namespace SystemVisitor;

public class FileSystemVisitor : IEnumerable<FileSystemInfo>
{
    private readonly DirectoryInfo _root;
    private readonly Predicate<string> _filter = x => true;
    public readonly EventHelper EventHelper = new EventHelper();

    public FileSystemVisitor(string root)
    {
        _root = CheckDirectory(root);
    }
    
    public FileSystemVisitor(string root, Predicate<string> filter)
    {
        _root = CheckDirectory(root);
        _filter = filter;
    }

    public IEnumerator<FileSystemInfo> GetEnumerator()
    {
        EventHelper.OnStart(new SearchEventArgs());
        foreach (var item in GetDirectoryItems(_root)) 
            yield return item; 
        EventHelper.OnFinish(new SearchEventArgs());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<FileSystemInfo> GetDirectoryItems(DirectoryInfo path)
    {
        SearchEventArgs eventArgs;
        foreach (var file in path.GetFiles())
        {
            eventArgs = new SearchEventArgs() { Current = file };
            if (_filter(file.ToString()))
            {
                EventHelper.OnFilteredFileFinded(eventArgs);
                if (eventArgs.Exclude)
                    continue;
                
                yield return file;
            }
            else
                EventHelper.OnFileFinded(eventArgs);

            if (eventArgs.Cancel)
                yield break;

            if (!_filter(file.ToString()) && eventArgs.Exclude) 
                yield return file;
        }
        
        foreach (var directory in path.GetDirectories())
        {
            eventArgs = new SearchEventArgs { Current = directory };
            if (_filter(directory.ToString()))
            {
                EventHelper.OnFilteredFileFinded(eventArgs);
                if (eventArgs.Exclude)
                    continue;
                
                yield return directory;
            }
            else
                EventHelper.OnDirectoryFinded(eventArgs);

            if (eventArgs.Cancel)
                yield break;
            if (!_filter(directory.ToString()) && eventArgs.Exclude) 
                yield return directory;

            foreach (var directoryItem in GetDirectoryItems(directory))
            {
                yield return directoryItem;
            }
        }
    }

    private DirectoryInfo CheckDirectory(string directory)
    {
        if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            throw new Exception("Invalid directory.");

        return new DirectoryInfo(directory);
    }
}