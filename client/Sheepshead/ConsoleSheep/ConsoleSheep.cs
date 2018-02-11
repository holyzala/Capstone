using System;
using SharedSheep.Table;

namespace ConsoleSheep
{
    class ConsoleSheep
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
            ITable table = new Table(Prompt);
        }

        static string Prompt(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }
    }
}
