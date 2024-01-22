using System.Configuration;

namespace FileWatcher.Configuration.Elements;

public class RuleElement : ConfigurationElement
{
    [ConfigurationProperty("template", IsKey = true, IsRequired = true)]
    public string Template => (string)base["template"];

    [ConfigurationProperty("destination", IsRequired = true)]
    public string Destination => (string)base["destination"];

    [ConfigurationProperty("modifyRule", DefaultValue = ModifyRuleType.None)]
    public ModifyRuleType ModifyRule => (ModifyRuleType)base["modifyRule"];
}