using System;

namespace Multitargeting
{
    public class GreetingConstructor
    {
        public static string SayTimeAndHello(string name)
        {
            return $"{DateTime.Now.ToLongTimeString()} Hello, {name}!";
        }
    }
}