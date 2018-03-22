using AndroidSheep.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AndroidSheep.Models.Buttons;
using System.Collections.Generic;
using SharedSheep.Table;
using SharedSheep.Player;

namespace AndroidSheep
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Table2D : Game
    {
        private GraphicsTable graphicsTable;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameContent gameContent;
        public Rectangle spriteRectangle;
        public Vector2 spritePosition;
        public static int screenHeight;
        public static int screenWidth;
        Background background;
        PlayedArea playedArea;
        private List<Component> _tableComponents;
        private List<Component> _gameComponents;
        public Table2D()
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
            background = new Background(gameContent.TableTop, screenWidth, screenHeight);
            playedArea = new PlayedArea(screenWidth, screenHeight);

            graphicsTable = new GraphicsTable(gameContent);

            var button = new Card(gameContent.EightOfClubs, Content.Load<SpriteFont>("Fonts/Font"), playedArea)
            {
                Position = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2 - 300, GraphicsDevice.Viewport.Bounds.Height / 2 + 100),
            };
           
            _gameComponents = new List<Component>
            {
                button,
                
            };
            button.Click += Button_Click;

            _tableComponents = new List<Component>();
            // Create a new SpriteBatch, which can be used to draw textures.


            // TODO: use this.Content to load your game content here
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            GraphicsDevice.Clear(Color.Black);

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
            _tableComponents = graphicsTable._tableContent;
            
            foreach(var component in _gameComponents)
            {
                component.Update(gameTime);
            }
            foreach(var component in _tableComponents)
            {
                component.Update(gameTime);
            }
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
            _tableComponents = graphicsTable._tableContent;

            background.Draw(gameTime, spriteBatch, screenHeight, screenWidth);

            spriteBatch.Begin();
            foreach (var component in _gameComponents)
            {
                component.Draw(gameTime, spriteBatch, screenWidth, screenHeight);
            }

            playedArea.Draw(gameTime, spriteBatch, screenHeight, screenWidth);

            foreach (var component in _tableComponents)
            {
                component.Draw(gameTime, spriteBatch, screenWidth, screenHeight);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

         
    }
}
