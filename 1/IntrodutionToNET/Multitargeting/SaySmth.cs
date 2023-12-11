using System;

namespace Multitargeting
{
    public class SaySmth
    {
        public static string SayTimeAndHello(string name)
        {
            return $"{DateTime.Now.ToLongTimeString()} \nHello, {name}!";
        }
    }
}