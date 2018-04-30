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
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using AndroidSheep.Models.States;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidSheep
{
    public class AndroidSheepGame : Microsoft.Xna.Framework.Game
    {
        #region Shared Sheep Items
        private ITable _table;
        private ThreadStart _mainThreadStart;
        private Thread _mainThread;
        #endregion

        public Dictionary<IPlayer, AndroidPlayer> PlayerGraphicsDict;
        public AndroidCard[] BlindList;

        #region Graphics
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GameContent GameContent;
        public int ScreenHeight;
        public int ScreenWidth;
        public AndroidBackground Background;
        public List<AndroidCard> PlayableCards;
        public AndroidCard[] PlayedCards;
        #endregion

        #region States
        public StateType State;
        private AndroidState _currentState;
        private AndroidPickingState _pickingState;
        private AndroidBlindState _blindState;
        private AndroidPlayState _playingState;
        private AndroidRoundOverState _roundOverState;
        #endregion

        public void ChangeState(AndroidState state)
        {
            _currentState = state;
        }

        public AndroidSheepGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            TouchPanel.EnabledGestures =
                GestureType.Hold |
                GestureType.Tap |
                GestureType.DoubleTap;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {   
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameContent = new GameContent(Content);
            Background = new AndroidBackground(GameContent.TableTop, ScreenWidth, ScreenHeight);
            BlindList = new AndroidCard[2];
            PlayedCards = new AndroidCard[6];

            _currentState = null;
            _pickingState = new AndroidPickingState(this, _graphics.GraphicsDevice, GameContent);
            _blindState = new AndroidBlindState(this, _graphics.GraphicsDevice, GameContent);
            _playingState = new AndroidPlayState(this, _graphics.GraphicsDevice, GameContent);
            _roundOverState = new AndroidRoundOverState(this, _graphics.GraphicsDevice, GameContent);

            LoadGame();
            _mainThreadStart = new ThreadStart(_table.Start);
            _mainThread = new Thread(_mainThreadStart);
            _mainThread.Start();      
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _currentState?.Update(gameTime);
            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Background.Draw(gameTime, _spriteBatch);
            _currentState?.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }

        private void LoadGame()
        {
            IPlayer host = new LocalPlayer("Me");
            _table = new Table(new LocalPlayer("Me"), Prompt);
            _table.AddPlayer(new SimpleBot("Bot1"));
            _table.AddPlayer(new SimpleBot("Bot2"));
            _table.AddPlayer(new SimpleBot("Bot3"));
            _table.AddPlayer(new SimpleBot("Bot4"));
        }

        private string Prompt(PromptType promptType, Dictionary<PromptData, object> data)
        {
            string prompt = "";
            ITrick trick = null;
            IPlayer player = null;
            IPlayer picker = null;
            switch (promptType)
            {
                case PromptType.CardsDealt:
                    PlayerGraphicsDict = new Dictionary<IPlayer, AndroidPlayer>();
                    var blinddata = (IBlind)data[PromptData.Blind];
                    foreach (var addPlayer in _table.Players)
                    {
                        AndroidPlayer hostGraphics = new AndroidPlayer(addPlayer);
                        PlayerGraphicsDict.Add(addPlayer, hostGraphics);
                    }
                    var blindLoc = 250;
                    var blindIndex = 0;
                    foreach (var addBlind in blinddata)
                    {
                        var cardtexture = GameContent.TextureDict[addBlind];
                        var cardGraphics = new AndroidCard(cardtexture, addBlind)
                        {
                            IsBlind = true,
                            Position = new Vector2(blindLoc, ScreenHeight / 4.0f)
                        };
                        BlindList[blindIndex] = cardGraphics;
                        blindIndex++;
                        blindLoc += 500;
                    }
                    foreach (var playerPair in PlayerGraphicsDict)
                    {
                        var playerGraphicsHand = playerPair;
                        var playerHand = playerPair.Key.Hand;
                        var screen = ScreenWidth / 2 - 500;
                        foreach (var card in playerHand)
                        {
                            var cardtexture = GameContent.TextureDict[card];
                            var cardGraphics = new AndroidCard(cardtexture, card)
                            {
                                Position = new Vector2(screen, ScreenHeight - 200)
                            };
                            playerGraphicsHand.Value.AddCardToHand(cardGraphics);
                            screen += 150;
                        }
                    }
                    ChangeState(new AndroidPreGameState(this, _graphics.GraphicsDevice, GameContent));
                    State = StateType.PreGame;
                    
                    break;
                    
                case PromptType.Pick:
                    ChangeState(_blindState);
                    State = StateType.Blind;
                    prompt = "yes"; 
                    break;

                case PromptType.PlayCard:
                    State = StateType.Playing;                   
                    picker = (IPlayer)data[PromptData.Picker];
                    player = (IPlayer)data[PromptData.Player];
                    trick = (ITrick)data[PromptData.Trick];
                    ChangeState(_playingState);
                    _playingState.SetTrick(trick);
                    var cards = (List<ICard>)data[PromptData.Cards];
                    PlayableCards = new List<AndroidCard>();
                    PlayerGraphicsDict.TryGetValue(player, out var playergraphics);
                    foreach (var card in cards)
                    {
                        if (playergraphics == null) continue;
                        foreach (var cardgraphics in playergraphics.PlayableCards)
                        {
                            if (!card.Equals(cardgraphics.Card)) continue;
                            PlayableCards.Add(cardgraphics);
                            cardgraphics.IsPlayable = true;
                            break;
                        }
                    }
                    prompt = _playingState.PickingPrompt();
                    if (prompt != "")
                    {
                        _playingState.IsTrickSet = false;
                        _playingState.LeaderboardSet = false;
                    }

                    break;

                case PromptType.PickBlind:
                    State = StateType.Picking;
                    player = (IPlayer)data[PromptData.Player];
                    var blind = (IBlind)data[PromptData.Blind];
                    _pickingState.AssignBlind(blind);
                    _pickingState.AssignPlayer(player);
                    ChangeState(_pickingState);
                    prompt = _pickingState.PickingPrompt();    
                    break;

                case PromptType.RoundOver:
                    trick = ((IRound)data[PromptData.Round]).Trick;
                    ChangeState(_roundOverState);
                    State = StateType.RoundOver;
                    _roundOverState.SetTrick(trick);
                    prompt = _roundOverState.PickingPrompt();
                    if (prompt != "")
                    {
                       _roundOverState = new AndroidRoundOverState(this, _graphics.GraphicsDevice, GameContent);
                    }
                    break;

                case PromptType.GameOver:
                    ChangeState(new AndroidGameOverState(this, _graphics.GraphicsDevice, GameContent));
                    State = StateType.GameOver;
                    var game = (IGame)data[PromptData.Game];
                    prompt = string.Format("Picker {0} got {1} points\n", game.Picker, game.GetPickerScore());
                    prompt += "Scoresheet:\n";
                    _table.Players.ForEach(playerIt =>
                    {
                        prompt += string.Format("{0}: {1}  ", playerIt, _table.ScrSheet.Scores[playerIt][_table.Games.Count - 1]);
                    });
                    prompt += "\n";
                    break;

                case PromptType.TableOver:
                    ChangeState(new AndroidTableOverState(this, _graphics.GraphicsDevice, GameContent));
                    State = StateType.TableOver;
                    prompt = "Totals:\n";
                    _table.Players.ForEach(playerIt =>
                    {
                        prompt += string.Format("{0}: {1}  ", playerIt, _table.ScrSheet.Total()[playerIt]);
                    });
                    break;

                case PromptType.CallUp:
                    prompt = "Would you like to call up? (yes/no): ";
                    break;

                case PromptType.CalledUp:
                    prompt = string.Format("\nPicker called up to {0}\n", _table.Games.Last().PartnerCard);
                    break;

                default:
                    return "";
            }

            return prompt;
        }
       
    }
}
