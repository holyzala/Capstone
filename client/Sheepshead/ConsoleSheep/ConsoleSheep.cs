using SharedSheep.Player;
using SharedSheep.Table;
using System;

namespace ConsoleSheep
{
    internal class ConsoleSheep
    {
        private static void Main(string[] args)
        {
            ITable table = new Table(new LocalPlayer("Me"), Prompt);
            table.AddPlayer(new SimpleBot("Bot1"));
            table.AddPlayer(new SimpleBot("Bot2"));
            table.AddPlayer(new SimpleBot("Bot3"));
            table.AddPlayer(new SimpleBot("Bot4"));
            table.Start();

            Console.ReadLine();
        }

        private static string Prompt(PromptType prompt_type)
        {
            switch (prompt_type)
            {
                case PromptType.Pick:
                    Console.WriteLine("Do you want to pick? (yes/no)");
                    return Console.ReadLine();

                default:
                    return "";
            }
        }
    }
}