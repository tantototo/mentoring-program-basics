namespace SystemVisitor;

public class EventHelper
{
    public event EventHandler<SearchEventArgs>? Start;
    public event EventHandler<SearchEventArgs>? Finish;
    public event EventHandler<SearchEventArgs>? FileFinded;
    public event EventHandler<SearchEventArgs>? DirectoryFinded;
    public event EventHandler<SearchEventArgs>? FilteredFileFinded;
    public event EventHandler<SearchEventArgs>? FilteredDirectoryFinded;

    protected internal virtual void OnStart(SearchEventArgs e)
    {
        e.Message = "Start";
        Start?.Invoke(this, e);
    }

    protected internal virtual void OnFinish(SearchEventArgs e)
    {
        e.Message = "Finish";
        Finish?.Invoke(this, e);
    }

    protected internal virtual void OnFileFinded(SearchEventArgs e)
    {
        e.Message = $"File: {e.Current}";
        FileFinded?.Invoke(this, e);
    }

    protected internal virtual void OnDirectoryFinded(SearchEventArgs e)
    {
        e.Message = $"Directory: {e.Current}";
        DirectoryFinded?.Invoke(this, e);
    }

    protected internal virtual void OnFilteredFileFinded(SearchEventArgs e)
    {
        e.Message = $"FilteredFile: {e.Current}";
        FilteredFileFinded?.Invoke(this, e);
    }

    protected internal virtual void OnFilteredDirectoryFinded(SearchEventArgs e)
    {
        e.Message = $"FilteredDirectory: {e.Current}";
        FilteredDirectoryFinded?.Invoke(this, e);
    }
}