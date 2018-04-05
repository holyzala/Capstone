using AndroidSheep.Models;
using AndroidSheep.Models.Buttons;
using AndroidSheep.Models.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Game;
using SharedSheep.Player;
using SharedSheep.Round;
using SharedSheep.Table;
using SharedSheep.Trick;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AndroidSheep.Models.States;

namespace AndroidSheep
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AndroidSheepGame : Microsoft.Xna.Framework.Game
    {
        public Dictionary<IPlayer, AndroidPlayer> _playerGraphicsDict;
        public List<AndroidCard> _blindList;

        private ITable table;
        ThreadStart MainThreadStart;
        Thread MainThread;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameContent gameContent;
        public static int screenHeight;
        public static int screenWidth;
        public AndroidBackground background;

        private AndroidState _currentState;
        private AndroidState _nextState;
        public StateType state;

        public void ChangeState(AndroidState state)
        {
            _nextState = state;
        }

        public AndroidSheepGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _playerGraphicsDict = new Dictionary<IPlayer, AndroidPlayer>();

            screenHeight = graphics.PreferredBackBufferHeight;
            screenWidth = graphics.PreferredBackBufferWidth;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = new GameContent(Content);
            background = new AndroidBackground(gameContent.TableTop, screenWidth, screenHeight);
            _blindList = new List<AndroidCard>();
            _currentState = null;
            LoadGame();
            MainThreadStart = new ThreadStart(table.Start);
            MainThread = new Thread(MainThreadStart);
            MainThread.Start();
            
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (_currentState != null)
            {
                _currentState.Update(gameTime);
            }
            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            background.Draw(gameTime, spriteBatch);
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            if (_currentState != null)
            {
                _currentState.Draw(gameTime, spriteBatch);
            }
            spriteBatch.Begin();

            foreach (var player in _playerGraphicsDict)
            {
                if (player.Key is LocalPlayer)
                {
                    var playerCards = player.Value.playableCards;
                    if (playerCards != null)
                    {
                        foreach (var card in playerCards)
                        {
                            card.Draw(gameTime, spriteBatch);
                        }
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void LoadGame()
        {
            IPlayer host = new LocalPlayer("Me");
            table = new Table(new LocalPlayer("Me"), Prompt);
            table.AddPlayer(new SimpleBot("Bot1"));
            table.AddPlayer(new SimpleBot("Bot2"));
            table.AddPlayer(new SimpleBot("Bot3"));
            table.AddPlayer(new SimpleBot("Bot4"));
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
            AndroidPlayer playerGraphics = null;
            bool currentTurn;

            switch (prompt_type)
            {
                case PromptType.CardsDealt:
                    
                    var blinddata = (IBlind)data[PromptData.Blind];
                    foreach (var addPlayer in table.Players)
                    {
                        AndroidPlayer hostGraphics = new AndroidPlayer(addPlayer);
                        _playerGraphicsDict.Add(addPlayer, hostGraphics);
                    }

                    var blindLoc = screenWidth / 2;
                    foreach (var addBlind in blinddata)
                    {
                        Texture2D cardtexture = gameContent.textureDict[addBlind];
                        AndroidCard cardGraphics = new AndroidCard(cardtexture);
                        cardGraphics.Position = new Vector2(blindLoc + 50, screenHeight / 4);
                        _blindList.Add(cardGraphics);
                        blindLoc -= 50;
                    }
                    foreach (var playerPair in _playerGraphicsDict)
                    {
                        var playerGraphicsHand = playerPair;
                        var playerHand = playerPair.Key.Hand;
                        var screen = screenWidth / 2 - 500;
                        foreach (var card in playerHand)
                        {
                            Texture2D cardtexture = gameContent.textureDict[card];
                            AndroidCard cardGraphics = new AndroidCard(cardtexture)
                            {
                                Position = new Vector2(screen, screenHeight - 200)
                            };
                            playerGraphicsHand.Value.AddCardToHand(cardGraphics);
                            screen += 150;
                        }
                    }
                    ChangeState(new AndroidPreGameState(this, graphics.GraphicsDevice, gameContent));
                    state = StateType.PreGame;
                    break;
                    

                case PromptType.Pick:
                    ChangeState(new AndroidBlindState(this, graphics.GraphicsDevice, gameContent));
                    while (state == StateType.Blind)
                    {

                    }
                    state = StateType.Blind;
                    blind = (IBlind)data[PromptData.Blind];
                    player = (IPlayer)data[PromptData.Player];  
                    
                    break;

                case PromptType.PlayCard:
                    ChangeState(new AndroidPlayState(this, graphics.GraphicsDevice, gameContent));
                    state = StateType.Playing;
                    while(state == StateType.Playing)
                    {

                    }
                    player = (IPlayer)data[PromptData.Player];
                    playerGraphics = _playerGraphicsDict[player];                  
                    picker = (IPlayer)data[PromptData.Picker];
                    player = (IPlayer)data[PromptData.Player];
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
                    player = (IPlayer)data[PromptData.Player];
                    playerGraphics = _playerGraphicsDict[player];
                    currentTurn = playerGraphics.thisPlayerTurn = true;
                    while (currentTurn)
                    {
                        foreach (var card in playerGraphics.playableCards)
                        {
                            card.canClick = true;
                        }
                    }
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
