using System.Configuration;
using FileWatcher.Configuration.Elements;

namespace FileWatcher.Configuration;

public class FileWatcherConfigurationSection : ConfigurationSection
{
    [ConfigurationProperty("appName")]
    public string Name
    {
        get { return (string)base["appName"]; }
    }
    
    [ConfigurationProperty("language", DefaultValue = "en-US")]
    public string Language
    {
        get { return (string)this["language"]; }
    }
    
    [ConfigurationCollection(typeof(FolderElement), AddItemName = "folder")]
    [ConfigurationProperty("folders")]
    public FolderElementCollection Folders
    {
        get { return (FolderElementCollection)this["folders"]; }
    }

    [ConfigurationProperty("defaultFolder")]
    public FolderElement DefaultFolder
    {
        get { return (FolderElement)this["defaultFolder"]; }
    }
    
    [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
    [ConfigurationProperty("rules")]
    public RuleElementCollection Rules
    {
        get { return (RuleElementCollection)this["rules"]; }
    }
}