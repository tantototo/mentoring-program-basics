namespace FileWatcher;

public class EventHelper
{
    public event EventHandler<FileEventArgs>? Created;
    public event EventHandler<FileEventArgs>? RuleFound;
    public event EventHandler<FileEventArgs>? RuleNotFound;
    public event EventHandler<FileEventArgs>? Modified;
    public event EventHandler<FileEventArgs>? Error;

    protected internal virtual void OnCreated(FileEventArgs args)
    {
        Created?.Invoke(this, args);
    }

    protected internal virtual void OnRuleFound(FileEventArgs args)
    {
        RuleFound?.Invoke(this, args);
    }

    protected internal virtual void OnRuleNotFound(FileEventArgs args)
    {
        RuleNotFound?.Invoke(this, args);
    }

    protected internal virtual void OnModify(FileEventArgs args)
    {
        Modified?.Invoke(this, args);
    }

    protected internal virtual void OnError(FileEventArgs args)
    {
        Error?.Invoke(this, args);
    }
}