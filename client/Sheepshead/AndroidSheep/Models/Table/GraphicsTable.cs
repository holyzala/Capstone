using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidSheep.Models.Buttons;
using Microsoft.Xna.Framework.Content;
using SharedSheep.Card;
using SharedSheep.Game;
using SharedSheep.Player;
using SharedSheep.Table;
using SharedSheep.Trick;

namespace AndroidSheep.Models
{
    internal class GraphicsTable
    {
        private ITable table;
        private GameContent _content;
        public List<Component> _tableContent { get; set; }
        public GraphicsTable(GameContent content)
        {
            _content = content;
            _tableContent = new List<Component>();
            table = new Table(new LocalPlayer("Me"), Prompt);
           table.AddPlayer(new SimpleBot("Bot1"));
           table.AddPlayer(new SimpleBot("Bot2"));
           table.AddPlayer(new SimpleBot("Bot3"));
           table.AddPlayer(new SimpleBot("Bot4"));
           table.Start();
        }

        private string Prompt(PromptType prompt_type)
        {
            ButtonPrompt prompt = null;
            int index;
            ITrick trick = null;
            IGame game = null;
            switch (prompt_type)
            {
                case PromptType.Pick:
                    prompt = new ButtonPrompt(PromptType.Pick, _content.KingOfClubs, _content.Font);
                    _tableContent.Add(prompt);
                    break;

                /*case PromptType.PlayCard:
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
                    prompt += "Scoresheet:\n";
                    foreach (IPlayer player in table.Players)
                    {
                        prompt += string.Format("{0}: {1}  ", player, table.ScrSheet.Scores[player][table.Games.Count - 1]);
                    }
                    prompt += "\n";
                    break;

                case PromptType.TableOver:
                    prompt = "Totals:\n";
                    foreach (IPlayer player in table.Players)
                    {
                        prompt += string.Format("{0}: {1}  ", player, table.ScrSheet.Total()[player]);
                    }
                    break;
                    */
                default:
                    return "";
            }
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}