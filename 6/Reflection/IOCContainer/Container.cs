using System.Reflection;
using IOCContainer.Attributes;

namespace IOCContainer;

public class Container
{
    private readonly Dictionary<Type, Type> types = new Dictionary<Type, Type>();

    public void AddAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        var allowedAttributes = new[] 
            { typeof(Export), typeof(Import), typeof(ImportConstructor) };
        
        foreach (var type in assembly.DefinedTypes)
        {
            var attributes = type.GetCustomAttributes()
                .Where(a => allowedAttributes.Contains(a.TypeId));
            
            if (!attributes.Any() || types.ContainsKey(type) || types.ContainsValue(type)) 
                continue;

            var baseType = type.GetCustomAttribute<Export>()?.BaseType;
            if (baseType != null)
                AddType(baseType, type);
            else
                AddType(type);
        }
    }

    public void AddType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        if (CheckType(type))
            types.TryAdd(type, type);
    }

    public void AddType(Type baseType, Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(baseType);
        if (CheckType(type))
            types.TryAdd(baseType, type);
    }

    private static bool CheckType(Type type)
    {
        if (type.IsAbstract || type.IsInterface)
            throw new Exception("Abstract class or interface can't be resolver");
        
        return true;
    }
    
    public TInterface Create<TInterface>()
    {
        return (TInterface)Create(typeof(TInterface));
    }

    public object Create(Type type)
    {
        if (type.IsAbstract || type.IsInterface)
            if (types.TryGetValue(type, out var resolver))
                return Activator.CreateInstance(resolver) ?? throw new ArgumentNullException();
        
        var constructor = type.GetConstructors().FirstOrDefault();
        var parameters = constructor?.GetParameters()
            .Select(param => Create(param.ParameterType)).ToArray();

        var result = constructor != null && parameters != null && parameters.Length != 0 ? 
            constructor?.Invoke(parameters) : Activator.CreateInstance(type);
        
        ArgumentNullException.ThrowIfNull(result);
        return result;
    }
}