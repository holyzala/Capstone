using AndroidSheep.Models.Buttons;
using AndroidSheep.Models.Player;
using AndroidSheep.Models.States;
using Microsoft.Xna.Framework.Graphics;
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

namespace AndroidSheep.Models
{
    public class AndroidTable
    {
        private ITable table;
        List<Tuple<IPlayer, AndroidPlayer>> _playerGraphicsPair;
        GameContent _gameContent;
        AndroidSheepGame _game;
        private AndroidState _currentState;
        private AndroidState _nextState;
        public void ChangeState(AndroidState state)
        {
            _nextState = state;
        }

        public AndroidTable(List<Tuple<IPlayer, AndroidPlayer>> playerGraphicsPair, GameContent gameContent, AndroidSheepGame game)
        {
            _playerGraphicsPair = playerGraphicsPair;
            _gameContent = gameContent;
            _game = game;
            IPlayer host = new LocalPlayer("Me");
            table = new Table(new LocalPlayer("Me"), Prompt);
            table.AddPlayer(new SimpleBot("Bot1"));
            table.AddPlayer(new SimpleBot("Bot2"));
            table.AddPlayer(new SimpleBot("Bot3"));
            table.AddPlayer(new SimpleBot("Bot4"));
            Console.ReadLine();
        }

        public void Start()
        {
            table.Start();

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
                case PromptType.CardsDealt:
                    foreach(var addPlayer in table.Players)
                    {
                        AndroidPlayer hostGraphics = new AndroidPlayer(addPlayer);
                        var playerOne = Tuple.Create(addPlayer, hostGraphics);
                        _playerGraphicsPair.Add(playerOne);
                    }

                    foreach (var playerPair in _playerGraphicsPair)
                    {
                        var playerGraphicsHand = playerPair.Item2;
                        var playerHand = playerPair.Item1.Hand;
                        foreach(var card in playerHand)
                        {
                            Texture2D cardtexture = _gameContent.KingOfHearts;
                            AndroidCard cardGraphics = new AndroidCard(cardtexture);
                            playerGraphicsHand.AddCardToHand(cardGraphics);
                        }
                    }
                    
                    break;

                case PromptType.Pick:
                    
                    prompt = "Your cards:\n";
                    player = (IPlayer)data[PromptData.Player];
                    foreach (ICard card in player.Hand)
                    {
                        prompt += string.Format("{0}\n", card);

                    };
                    prompt += "\nDo you want to pick? (yes/no)\n";
                    break;

                case PromptType.PlayCard:
                    picker = (IPlayer)data[PromptData.Picker];
                    player = (IPlayer)data[PromptData.Player];
                    var playerPair2 = _playerGraphicsPair.Where(pair => pair.Item1 == player);
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
                    return "";
            }
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}