using SharedSheep.Player;
using SharedSheep.Table;
using System;
using System.Collections.Generic;
using SharedSheep.Card;
using SharedSheep.Game;
using SharedSheep.Trick;
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
            string prompt = "";
            int index;
            ITrick trick = null;
            IGame game = null;
            switch (prompt_type)
            {
                case PromptType.Pick:
                    Console.Clear();
                    prompt = "Your cards:\n";
                    prompt += table.Players[0].Hand.Cards.Aggregate("", (finished, next) => string.Format("{0}{1}\n", finished, next));
                    prompt += "\nDo you want to pick? (yes/no)\n";
                    break;

                case PromptType.PlayCard:
                    Console.Clear();
                    prompt = "Picker: " + table.Games.Last().Picker + "\n";
                    prompt += "Cards Played\n";
                    trick = table.Games.Last().Rounds.Last().Trick;
                    prompt += trick.TrickCards.Aggregate("", (finished, next) => string.Format("{0}{1}\n", finished, next));
                    prompt += "\nYour playable cards:\n";
                    List<ICard> cards = table.GetCurrentPlayer().Hand.GetPlayableCards(trick.LeadingCard());
                    index = 0;
                    prompt += cards.Aggregate("", (finished, next) => string.Format("{0}{1}) {2}\n", finished, index++, next));
                    break;

                case PromptType.PickBlind:
                    Console.Clear();
                    prompt = "Blind cards:\n";
                    game = table.Games.Last();
                    index = 0;
                    prompt += game.Blind.BlindCards.Aggregate("", (finished, next) => string.Format("{0}{1}) {2}\n", finished, index++, next));
                    prompt += "\nYour Cards:\n";
                    index = 0;
                    prompt += table.Players[0].Hand.Cards.Aggregate("", (finished, next) => string.Format("{0}{1}) {2}\n", finished, index++, next));
                    break;

                case PromptType.RoundOver:
                    trick = table.Games.Last().Rounds.Last().Trick;
                    prompt = string.Format("{0} won the trick for {1} points\n", trick.TheWinnerPlayer(), trick.TrickValue());
                    prompt += "All cards played:\n";
                    prompt += trick.TrickCards.Aggregate("", (finished, next) => string.Format("{0}{1}\n", finished, next));
                    break;

                case PromptType.GameOver:
                    game = table.Games.Last();
                    prompt = string.Format("Picker {0} got {1} points\n", game.Picker, game.GetPickerScore());
                    break;

                default:
                    Console.Clear();
                    return "";
            }
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}