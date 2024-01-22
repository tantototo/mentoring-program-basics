using System.Configuration;
using System.Globalization;
using FileWatcher;
using FileWatcher.Configuration;
using messages = FileWatcher.Resources.Messages;

var eventHelper = new EventHelper();
eventHelper.Created += OnActivity;
eventHelper.RuleFound += OnActivity;
eventHelper.RuleNotFound += OnActivity;
eventHelper.Modified += OnActivity;
eventHelper.Error += OnActivity;

FileWatcherConfigurationSection configuration;
try
{
    configuration = (FileWatcherConfigurationSection) 
        ConfigurationManager.GetSection("fileWatcherConfigurationSection");
}
catch(Exception e)
{
    eventHelper.OnError(new FileEventArgs(){ Message = messages.ConfigurationException });
    throw new Exception(e.Message);
}

var culture = new CultureInfo(configuration.Language);
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

try
{
    var folderWatcher = new FolderWatcher(configuration, eventHelper);
    folderWatcher.Watch();
}
catch(Exception e)
{
    eventHelper.OnError(new FileEventArgs(){ Message = messages.CantWatchException });
    throw new Exception(e.Message);
}
Console.ReadLine();

void OnActivity(object? o, FileEventArgs args) => Console.WriteLine(args.Message);