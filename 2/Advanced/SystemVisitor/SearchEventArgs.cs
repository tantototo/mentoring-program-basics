namespace SystemVisitor;

public class SearchEventArgs : EventArgs
{
    public FileSystemInfo Current { get; set; }
    public bool Exclude { get; set; }
    public bool Cancel { get; set; }
    public string Message { get; set; }
}