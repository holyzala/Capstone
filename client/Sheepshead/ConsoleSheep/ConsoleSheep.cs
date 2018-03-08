using SharedSheep.Card;
using SharedSheep.Game;
using SharedSheep.Player;
using SharedSheep.Table;
using SharedSheep.Trick;
using System;
using System.Collections.Generic;
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

        private string Prompt(PromptType prompt_type, object data)
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
                    table.Players[0].Hand.Cards.ForEach(card =>
                    {
                        prompt += string.Format("{0}\n", card);
                    });
                    prompt += "\nDo you want to pick? (yes/no)\n";
                    break;

                case PromptType.PlayCard:
                    Console.Clear();
                    prompt = string.Format("Picker: {0}\n", table.Games.Last().Picker);
                    prompt += "Cards Played\n";
                    trick = table.Games.Last().Rounds.Last().Trick;
                    trick.TrickCards.ForEach(card =>
                    {
                        prompt += string.Format("{0}\n", card);
                    });
                    prompt += "\nYour playable cards:\n";
                    List<ICard> cards = table.GetCurrentPlayer().Hand.GetPlayableCards(trick.LeadingCard());
                    index = 0;
                    cards.ForEach(card =>
                    {
                        prompt += string.Format("{0}) {1}\n", index++, card);
                    });
                    break;

                case PromptType.PickBlind:
                    Console.Clear();
                    prompt = "Blind cards:\n";
                    game = table.Games.Last();
                    index = 0;
                    game.Blind.BlindCards.ForEach(card =>
                    {
                        prompt += string.Format("{0}) {1}\n", index++, card);
                    });
                    prompt += "\nYour Cards:\n";
                    index = 0;
                    table.Players[0].Hand.Cards.ForEach(card =>
                    {
                        prompt += string.Format("{0}) {1}\n", index++, card);
                    });
                    break;

                case PromptType.RoundOver:
                    trick = table.Games.Last().Rounds.Last().Trick;
                    prompt = string.Format("{0} won the trick for {1} points\n", trick.TheWinnerPlayer(), trick.TrickValue());
                    prompt += "All cards played:\n";
                    trick.TrickCards.ForEach(card =>
                    {
                        prompt += string.Format("{0}\n", card);
                    });
                    break;

                case PromptType.GameOver:
                    game = table.Games.Last();
                    prompt = string.Format("Picker {0} got {1} points\n", game.Picker, game.GetPickerScore());
                    prompt += "Scoresheet:\n";
                    table.Players.ForEach(player =>
                    {
                        prompt += string.Format("{0}: {1}  ", player, table.ScrSheet.Scores[player][table.Games.Count - 1]);
                    });
                    prompt += "\n";
                    break;

                case PromptType.TableOver:
                    prompt = "Totals:\n";
                    table.Players.ForEach(player =>
                    {
                        prompt += string.Format("{0}: {1}  ", player, table.ScrSheet.Total()[player]);
                    });
                    break;

                case PromptType.CallUp:
                    prompt = "Would you like to call up? (yes/no): ";
                    break;

                case PromptType.CalledUp:
                    prompt = string.Format("\nPicker called up to {0}\n", table.Games.Last().PartnerCard);
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