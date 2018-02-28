using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using iface = AndroidSheep.Models;

namespace AndroidSheep
{
    public class Table : Game
    {
        GraphicsDeviceManager graphics;
        BasicEffect effect;
        Texture2D checkerboard;
        Texture2D queenofspades;
        iface.TableTop table;
        VertexPositionNormalTexture[] cardverts;

        Vector3 cameraPosition = new Vector3(0, 24.5f, 7);
        iface.DebugCamera camera;
        int numPlayers = 5;

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
            camera = new iface.DebugCamera(cameraPosition, graphics);
            table = new iface.TableTop(graphics, effect);
            table.InitializeTable();
            initializeCards();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            checkerboard = Content.Load<Texture2D>("Table/darktexture");
            queenofspades = Content.Load<Texture2D>("Spades/queen_of_spades");

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

        public void initializeCards()
        {
            cardverts = new VertexPositionNormalTexture[6];
            
            cardverts[0].Position = new Vector3(-.5f, 13, .1f);
            cardverts[1].Position = new Vector3(-.5f, 15, .1f);
            cardverts[2].Position = new Vector3(.5f, 13, .1f);
            cardverts[3].Position = cardverts[1].Position;
            cardverts[4].Position = new Vector3(.5f, 15, .1f);
            cardverts[5].Position = cardverts[2].Position;

            cardverts[0].TextureCoordinate = new Vector2(0, 0);
            cardverts[1].TextureCoordinate = new Vector2(0, 1);
            cardverts[2].TextureCoordinate = new Vector2(1, 0);
            cardverts[3].TextureCoordinate = new Vector2(0, 1);
            cardverts[4].TextureCoordinate = new Vector2(1, 1);

            cardverts[5].TextureCoordinate = new Vector2(1, 0);
        }

        public void DrawCard(Texture2D texture)
        {
            this.effect.TextureEnabled = true;
            this.effect.Texture = texture;
            foreach (var pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, cardverts, 0, 2);
            }
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

            table.DrawGround(checkerboard);
            DrawCard(queenofspades);
            base.Draw(gameTime);
        }
    }
}