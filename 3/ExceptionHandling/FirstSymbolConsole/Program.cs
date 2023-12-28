
Console.Write("Input string: ");
var s = Console.ReadLine();

if(string.IsNullOrEmpty(s))
    throw new ArgumentNullException(s);
if(string.IsNullOrWhiteSpace(s))
    throw new ArgumentException("Only white space is not allow.");

Console.WriteLine(s.FirstOrDefault()); 