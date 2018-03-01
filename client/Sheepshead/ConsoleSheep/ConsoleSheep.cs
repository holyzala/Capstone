using SharedSheep.Player;
using SharedSheep.Table;
using System;
using System.Collections.Generic;
using SharedSheep.Card;
using System.Linq;

namespace ConsoleSheep
{
    internal class ConsoleSheep
    {
        private ITable table;

        private static void Main(string[] args)
        {
            ConsoleSheep c = new ConsoleSheep();
            c.table = new Table(new LocalPlayer("Me"), c.Prompt);
            c.table.AddPlayer(new SimpleBot("Bot1"));
            c.table.AddPlayer(new SimpleBot("Bot2"));
            c.table.AddPlayer(new SimpleBot("Bot3"));
            c.table.AddPlayer(new SimpleBot("Bot4"));
            c.table.Start();

            Console.ReadLine();
        }

        private string Prompt(PromptType prompt_type)
        {
            switch (prompt_type)
            {
                case PromptType.Pick:
                    Console.WriteLine("Do you want to pick? (yes/no)");
                    return Console.ReadLine();

                case PromptType.PlayCard:
                    List<ICard> cards = table.GetCurrentPlayer().Hand.GetPlayableCards(table.Games.Last().Rounds.Last().Trick.LeadingCard());
                    string stuff = cards.Aggregate("", (finished, next) => finished + "\n" + next.ToString());
                    Console.WriteLine(stuff);
                    return Console.ReadLine();

                default:
                    return "";
            }
        }
    }
}