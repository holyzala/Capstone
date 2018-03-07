using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AndroidSheep.Models;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace AndroidSheep
{
    public class Table : Game
    {
        GraphicsDeviceManager graphics;
        BasicEffect effect;

        Texture2D checkerboard;
        Texture2D queenofspades;
        SpriteBatch spriteBatch;

        TableTop table;

        Card[] deck;

        DebugCamera camera;
        Vector3 debugCameraPosition = new Vector3(0, 10, 8);
        Vector3 cardPosition = new Vector3(.33f, 10, 8);

        List<PlayerCamera> playerCameraLocations;
        List<Player> players;

        public Table()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
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
            effect = new BasicEffect(graphics.GraphicsDevice);
            camera = new DebugCamera(debugCameraPosition, graphics);
            table = new TableTop(graphics, effect);

            

            playerCameraLocations = new List<PlayerCamera>();
            players = new List<Player>();

            table.InitializeTable();
            //InitializePlayerCameras();
            InitializeDeck();
            //InitializePlayers();
            base.Initialize();
        }

       
        private void InitializeDeck()
        {
           

        }

        /*private void InitializePlayerCameras()
        {
            Vector3 playerVectorOne;
            Vector3 playerVectorTwo;
            for(int i = 3; i < (numPlayers * 3) + 1; i = i + 3)
            {
                playerVectorOne = table.GetFloorVertex(i - 3);
                playerVectorTwo = table.GetFloorVertex(i - 1);
                var playerYValue = (playerVectorOne.X + playerVectorTwo.X) / 2;
                var playerXValue = (- playerVectorOne.Y - playerVectorTwo.Y) / 2;

                Vector3 playerStartVector = new Vector3(playerXValue, playerYValue, 0);
                PlayerCamera playerCamera = new PlayerCamera(playerStartVector, graphics);
                playerCameraLocations.Add(playerCamera);

                Debug.WriteLine(string.Format("PlayerVector X, Y: {0}, {1}", playerStartVector.X, playerStartVector.Y));
            }           
        }*/

        /*private void InitializePlayers()
        {
            for (int i = 0; i < numPlayers; i++)
            {
                Player player = new Player(playerCameraLocations.ElementAt(i), effect, graphics);
                
                players.Add(player);
            }
        }*/

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            checkerboard = Content.Load<Texture2D>("Table/darktexture");
            queenofspades = Content.Load<Texture2D>("Clubs/10_of_clubs");


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
            camera.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;
            effect.World = Matrix.Identity;
            Vector2 position = new Vector2(150,500);
            Vector2 addVector = new Vector2(100, 0);
            table.DrawGround(checkerboard);
            for (int i = 0; i < 6; i++)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(queenofspades, position + addVector, null, Color.White, 0f,
                Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                spriteBatch.End();
                addVector.X += 100;
            }
           
            base.Draw(gameTime);
        }
    }
}
