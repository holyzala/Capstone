using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Game;
using SharedSheep.Player;
using SharedSheep.Round;
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
            c.table.AddPlayer(new EasyBot("Bot3"));
            c.table.AddPlayer(new EasyBot("Bot4"));
            c.table.Start();

            Console.ReadLine();
        }

        private string Prompt(PromptType prompt_type, Dictionary<PromptData, object> data)
        {
            string prompt = "";
            int index;
            ITrick trick = null;
            IGame game = null;
            IPlayer player = null;
            IPlayer picker = null;
            IBlind blind = null;
            switch (prompt_type)
            {
                case PromptType.Pick:
                    Console.Clear();
                    prompt = "Your cards:\n";
                    player = (IPlayer)data[PromptData.Player];
                    foreach (ICard card in player.Hand)
                    {
                        prompt += string.Format("{0}\n", card);
                    };
                    prompt += "\nDo you want to pick? (yes/no)\n";
                    break;

                case PromptType.PlayCard:
                    Console.Clear();
                    picker = (IPlayer)data[PromptData.Picker];
                    prompt = string.Format("Picker: {0}\n", picker);
                    prompt += "Cards Played\n";
                    trick = (ITrick)data[PromptData.Trick];
                    foreach ((IPlayer, ICard) playerCard in trick)
                    {
                        prompt += string.Format("{0}\n", playerCard);
                    };
                    prompt += "\nYour playable cards:\n";
                    List<ICard> cards = (List<ICard>)data[PromptData.Cards];
                    index = 0;
                    cards.ForEach(card =>
                    {
                        prompt += string.Format("{0}) {1}\n", index++, card);
                    });
                    break;

                case PromptType.PickBlind:
                    Console.Clear();
                    prompt = "Blind cards:\n";
                    blind = (IBlind)data[PromptData.Blind];
                    index = 0;
                    foreach (ICard card in blind)
                    {
                        prompt += string.Format("{0}) {1}\n", index++, card);
                    };
                    prompt += "\nYour Cards:\n";
                    index = 0;
                    player = (IPlayer)data[PromptData.Player];
                    foreach (ICard card in player.Hand)
                    {
                        prompt += string.Format("{0}) {1}\n", index++, card);
                    };
                    break;

                case PromptType.RoundOver:
                    trick = ((IRound)data[PromptData.Round]).Trick;
                    prompt = string.Format("{0} won the trick for {1} points\n", trick.TheWinnerPlayer(), trick.TrickValue());
                    prompt += "All cards played:\n";
                    foreach ((IPlayer, ICard) playerCard in trick)
                    {
                        prompt += string.Format("{0}\n", playerCard);
                    };
                    break;

                case PromptType.GameOver:
                    game = (IGame)data[PromptData.Game];
                    prompt = string.Format("Picker {0} got {1} points\n", game.Picker, game.GetPickerScore());
                    prompt += "Scoresheet:\n";
                    table.Players.ForEach(playerIt =>
                    {
                        prompt += string.Format("{0}: {1}  ", playerIt, table.ScrSheet.Scores[playerIt][table.Games.Count - 1]);
                    });
                    prompt += "\n";
                    break;

                case PromptType.TableOver:
                    prompt = "Totals:\n";
                    table.Players.ForEach(playerIt =>
                    {
                        prompt += string.Format("{0}: {1}  ", playerIt, table.ScrSheet.Total()[playerIt]);
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