namespace FileWatcher;

public class FileEventArgs : EventArgs //FileSystemEventArgs
{
    public string? Message { get; set; }

    // public FileEventArgs(WatcherChangeTypes changeType, string directory, string? name) 
    //     : base(changeType, directory, name)
    // {
    // }
}