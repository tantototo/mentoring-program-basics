using Multitargeting;


var name = "";
while (name == "")
{
    Console.Write("Input name: ");
    name = Console.ReadLine();
}
Console.WriteLine(SaySmth.SayTimeAndHello(name));
