using Multitargeting;

var name = string.Empty;
if (args.Length != 0)
{
    name = args[0];
}
else
{
    while (string.IsNullOrEmpty(name))
    {
        Console.Write("Input name: ");
        name = Console.ReadLine();
    }
}
Console.WriteLine(GreetingConstructor.SayTimeAndHello(name));
