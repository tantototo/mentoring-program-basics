using System.Configuration;

namespace FileWatcher.Configuration.Elements;

public class FolderElement : ConfigurationElement
{
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
        get => (string)base["name"];
        set => base["name"] = value;
    }
    
    [ConfigurationProperty("path", IsRequired = true)]
    public string Path
    {
        get => (string)base["path"];
        set => base["path"] = value;
    }
}