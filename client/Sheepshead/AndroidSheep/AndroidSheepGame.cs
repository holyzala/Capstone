using AndroidSheep.Models;
using AndroidSheep.Models.Buttons;
using AndroidSheep.Models.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedSheep.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AndroidSheep
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AndroidSheepGame : Game
    {
        public List<Tuple<IPlayer, AndroidPlayer>> playerDataGraphicsPair;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameContent gameContent;
        public Rectangle spriteRectangle;
        public Vector2 spritePosition;
        public static int screenHeight;
        public static int screenWidth;
        AndroidBackground background;
        AndroidPlayedArea  playedArea;
        AndroidTable _playTable;
        private List<AndroidComponent> _tableComponents;
        private List<AndroidComponent> _gameComponents;
        public List<AndroidPlayer> _gamePlayers;

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
            playerDataGraphicsPair = new List<Tuple<IPlayer, AndroidPlayer>>();

            _playTable = new AndroidTable(playerDataGraphicsPair);
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
            _gameComponents = new List<AndroidComponent>();

            _playTable.Start();
            // Create a new SpriteBatch, which can be used to draw textures.


            // TODO: use this.Content to load your game content here
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
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            background.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();
            foreach(var player in playerDataGraphicsPair)
            {
                var playerCards = player.Item2.playableCards;
                if (playerCards != null)
                {
                    foreach (var card in playerCards)
                    {
                        card.Draw(gameTime, spriteBatch);
                    }
                }
            }
           
            spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

        
    }
}
