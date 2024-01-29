namespace IOCContainer.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class Export : Attribute
{
    public Type? BaseType { get; private set; }
    
    public Export(){}

    public Export(Type type)
    {
        BaseType = type;
    }
}