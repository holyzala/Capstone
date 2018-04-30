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
using System.Collections.Generic;
using System.Linq;
using AndroidSheep.Models.States;
using Microsoft.Xna.Framework.Input.Touch;
using SharedSheep.ScoreSheet;

namespace AndroidSheep
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AndroidSheepGame : Microsoft.Xna.Framework.Game
    {
        public Dictionary<IPlayer, AndroidPlayer> _playerGraphicsDict;
        public AndroidCard[] blindList;

        private ITable table;
        ThreadStart MainThreadStart;
        Thread MainThread;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public GameContent gameContent;
        public int screenHeight;
        public int screenWidth;
        public AndroidBackground background;
        public List<AndroidCard> playableCards;
        private AndroidState _currentState;
        public StateType state;
        public AndroidCard[] playedCards;
        private AndroidPickingState pickingState;
        private AndroidBlindState blindState;
        private AndroidPlayState playingState;
        private AndroidRoundOverState roundOverState;

        public void ChangeState(AndroidState state)
        {
            _currentState = state;
        }

        public AndroidSheepGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 100.0f);
            graphics.SynchronizeWithVerticalRetrace = false;

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
            TouchPanel.EnabledGestures =
                GestureType.Hold |
                GestureType.Tap |
                GestureType.DoubleTap;
            screenHeight = graphics.PreferredBackBufferHeight;
            screenWidth = graphics.PreferredBackBufferWidth;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
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
            blindList = new AndroidCard[2];
            playedCards = new AndroidCard[6];

            _currentState = null;
            pickingState = new AndroidPickingState(this, graphics.GraphicsDevice, gameContent);
            blindState = new AndroidBlindState(this, graphics.GraphicsDevice, gameContent);
            playingState = new AndroidPlayState(this, graphics.GraphicsDevice, gameContent);
            roundOverState = new AndroidRoundOverState(this, graphics.GraphicsDevice, gameContent);
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
            if (_currentState != null)
            {
                _currentState.Draw(gameTime, spriteBatch);
            }
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
            ITrick trick = null;
            IGame game = null;
            IPlayer player = null;
            IPlayer picker = null;
            IBlind blind = null;
            IScoreSheet score = null;
            switch (prompt_type)
            {
                case PromptType.CardsDealt:
                    _playerGraphicsDict = new Dictionary<IPlayer, AndroidPlayer>();
                    var blinddata = (IBlind)data[PromptData.Blind];
                    foreach (var addPlayer in table.Players)
                    {
                        AndroidPlayer hostGraphics = new AndroidPlayer(addPlayer);
                        _playerGraphicsDict.Add(addPlayer, hostGraphics);
                    }
                    var blindLoc = 250;
                    int blindIndex = 0;
                    foreach (var addBlind in blinddata)
                    {
                        Texture2D cardtexture = gameContent.textureDict[addBlind];
                        AndroidCard cardGraphics = new AndroidCard(cardtexture, addBlind);
                        cardGraphics.IsBlind = true;
                        cardGraphics.Position = new Vector2(blindLoc, screenHeight / 4);
                        blindList[blindIndex] = cardGraphics;
                        blindIndex++;
                        blindLoc += 500;
                    }
                    foreach (var playerPair in _playerGraphicsDict)
                    {
                        var playerGraphicsHand = playerPair;
                        var playerHand = playerPair.Key.Hand;
                        var screen = screenWidth / 2 - 500;
                        foreach (var card in playerHand)
                        {
                            Texture2D cardtexture = gameContent.textureDict[card];
                            AndroidCard cardGraphics = new AndroidCard(cardtexture, card)
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
                    ChangeState(blindState);
                    state = StateType.Blind;
                    prompt = "yes"; 
                    break;

                case PromptType.PlayCard:
                    state = StateType.Playing;                   
                    picker = (IPlayer)data[PromptData.Picker];
                    player = (IPlayer)data[PromptData.Player];
                    trick = (ITrick)data[PromptData.Trick];
                    ChangeState(playingState);
                    playingState.SetTrick(trick);
                    playingState.SetScore(score);
                    var cards = (List<ICard>)data[PromptData.Cards];
                    playableCards = new List<AndroidCard>();
                    _playerGraphicsDict.TryGetValue(player, out var playergraphics);
                    foreach (var card in cards)
                    {
                        foreach (var cardgraphics in playergraphics.playableCards)
                        {
                            if (card.Equals(cardgraphics._card))
                            {
                                playableCards.Add(cardgraphics);
                                cardgraphics.isPlayable = true;
                                break;
                            }
                        }
                    }
                    prompt = playingState.PickingPrompt();
                    if (prompt != "")
                    {
                        playingState.isTrickSet = false;
                        playingState.LeaderboardSet = false;
                    }

                    break;

                case PromptType.PickBlind:
                    state = StateType.Picking;
                    player = (IPlayer)data[PromptData.Player];
                    blind = (IBlind)data[PromptData.Blind];
                    pickingState.assignBlind(blind);
                    pickingState.assignPlayer(player);
                    ChangeState(pickingState);
                    prompt = pickingState.PickingPrompt();    
                    break;

                case PromptType.RoundOver:
                    trick = ((IRound)data[PromptData.Round]).Trick;
                    ChangeState(roundOverState);
                    state = StateType.RoundOver;
                    roundOverState.SetTrick(trick);
                    prompt = roundOverState.PickingPrompt();
                    if (prompt != "")
                    {
                       roundOverState = new AndroidRoundOverState(this, graphics.GraphicsDevice, gameContent);
                    }
                    break;

                case PromptType.GameOver:
                    ChangeState(new AndroidGameOverState(this, graphics.GraphicsDevice, gameContent));
                    state = StateType.GameOver;
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
                    ChangeState(new AndroidTableOverState(this, graphics.GraphicsDevice, gameContent));
                    state = StateType.TableOver;
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

            return prompt;
        }
       
    }
}
