﻿using System.Configuration;

namespace FileWatcher.Configuration.Elements;

public class RuleElementCollection : ConfigurationElementCollection
{
    protected override ConfigurationElement CreateNewElement()
    {
        return new RuleElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
        return ((RuleElement)element).Template;
    }
}