using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle shipRect;
        Texture2D ship;
        Texture2D rock;
        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            shipRect.X = 465;
            shipRect.Y = 920;
            shipRect.Width = 70;
            shipRect.Height = 80;


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ship = Content.Load<Texture2D>("si");
            //rock = Content.Load<Texture2D>("ball");
                
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();
            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                shipRect.X -= 5;
            }

            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                shipRect.X += 5;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MediumPurple);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(ship, shipRect, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
