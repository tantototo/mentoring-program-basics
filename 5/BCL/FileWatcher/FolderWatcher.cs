using System.Globalization;
using System.Text.RegularExpressions;
using FileWatcher.Configuration;
using FileWatcher.Configuration.Elements;
using messages = FileWatcher.Resources.Messages;

namespace FileWatcher;

public class FolderWatcher
{
    private readonly FileWatcherConfigurationSection _configuration;
    private readonly EventHelper? _eventHelper;
    
    public FolderWatcher(FileWatcherConfigurationSection configuration)
    {
        _configuration = configuration;
    }
    
    public FolderWatcher(FileWatcherConfigurationSection configuration,
        EventHelper eventHelper): this(configuration)
    {
        _eventHelper = eventHelper;
    }

    public void Watch()
    {
        var fileWatchers = (from FolderElement folder in _configuration.Folders 
            select new FileSystemWatcher(folder.Path)).ToList();

        foreach (var watcher in fileWatchers)
        {
            watcher.Created += OnChange;
            watcher.Changed += OnChange;
            watcher.Renamed += OnChange;
            watcher.EnableRaisingEvents = true;
        }
    }

    protected virtual void OnChange(object o, FileSystemEventArgs args)
    {
        var fileInfo = new FileInfo(args.FullPath);

        if (fileInfo.Attributes.Equals(FileAttributes.Directory) || 
            !File.Exists(fileInfo.FullName))
            return;
        
        if ((args.ChangeType & WatcherChangeTypes.Created) != 0)
        {
            _eventHelper?.OnCreated(new FileEventArgs() 
                { Message = $"{messages.FileCreated} {fileInfo.FullName}" });
        }

        var rule = _configuration.Rules.Cast<RuleElement>()
            .FirstOrDefault(rule => new Regex(rule.Template).Match(args.Name).Success);

        if (rule == null)
        {
            _eventHelper?.OnRuleNotFound(new FileEventArgs() { Message = messages.RuleNotFound });
            MoveFile(fileInfo, _configuration.DefaultFolder.Path);
        }
        else if (fileInfo.DirectoryName != null && 
                 !fileInfo.DirectoryName.Equals(rule.Destination))
        {
            _eventHelper?.OnRuleFound(new FileEventArgs() { Message = messages.RuleFound });
            MoveFile(fileInfo, rule.Destination);
            RenameFile(fileInfo, rule.ModifyRule);
        }
        
        _eventHelper?.OnModify(new FileEventArgs() 
            { Message = $"{messages.FileWasChangedOrMoved} {fileInfo.FullName}" });
    }

    private void MoveFile(FileInfo fileInfo, string newPath)
    {
        if (string.IsNullOrEmpty(newPath))
        {
            _eventHelper?.OnError(new FileEventArgs() { Message = messages.FileDestinationNullException });
            throw new ArgumentNullException();
        }

        if (!Directory.Exists(newPath))
            Directory.CreateDirectory(newPath);

        try
        {
            fileInfo.MoveTo(newPath + $"\\{fileInfo.Name}");
        } 
        catch (Exception e)
        {
            _eventHelper?.OnError(new FileEventArgs() { Message = messages.FileNotMovedException });
            throw new Exception(e.Message);
        }
    }

    private void RenameFile(FileInfo fileInfo, ModifyRuleType type)
    {
        var newName = string.Empty;

        switch (type)
        {
            case ModifyRuleType.ModifyDate:
                newName = $" {DateTime.Now.ToString("D", CultureInfo.InvariantCulture)}";
                break;
            case ModifyRuleType.SerialNumber:
                var serialNumber = Console.ReadLine();
                while (string.IsNullOrEmpty(serialNumber) || string.IsNullOrWhiteSpace(serialNumber))
                    serialNumber = Console.ReadLine();
                
                newName = $" {serialNumber}";
                break;
            default:
                return;
        }
        
        try
        {
            if (fileInfo.DirectoryName != null)
                File.Move(fileInfo.FullName, Path.Combine(fileInfo.DirectoryName,
                    fileInfo.Name + newName));
        }
        catch (Exception e)
        {
            _eventHelper?.OnError(new FileEventArgs() { Message = messages.FileRenameException });
            throw new Exception(e.Message);
        }

    }
}