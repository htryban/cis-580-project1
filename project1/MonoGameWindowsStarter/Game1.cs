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
        int progression = 0;
        int score = 0;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand = new Random();
        Rectangle shipRect;
        Rectangle meteorRect1;
        Rectangle meteorRect2;
        Rectangle meteorRect3;
        Rectangle meteorRect4;
        Rectangle meteorRect5;
        Texture2D ship;
        Texture2D rock;
        //KeyboardState oldKeyboardState;
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

            shipRect.X = 550;
            shipRect.Y = 900;
            shipRect.Width = 70;
            shipRect.Height = 80;

            meteorRect1.X = rand.Next(900, 1150);
            meteorRect1.Y = rand.Next(-200, -1);
            meteorRect1.Width = 50;
            meteorRect1.Height = 50;

            meteorRect2.X = rand.Next(650, 900);
            meteorRect2.Y = -100;
            meteorRect2.Width = 50;
            meteorRect2.Height = 50;

            meteorRect3.X = rand.Next(450, 650);
            meteorRect3.Y = rand.Next(-200, -1);
            meteorRect3.Width = 50;
            meteorRect3.Height = 50;

            meteorRect4.X = rand.Next(250, 450);
            meteorRect4.Y = -300;
            meteorRect4.Width = 50;
            meteorRect4.Height = 50;

            meteorRect5.X = rand.Next(250);
            meteorRect5.Y = rand.Next(-200, -1);
            meteorRect5.Width = 50;
            meteorRect5.Height = 50;

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
            rock = Content.Load<Texture2D>("meteor");
                
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
                shipRect.X -= 10;
            }

            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                shipRect.X += 10;
            }

            if(newKeyboardState.IsKeyDown(Keys.Space) && newKeyboardState.IsKeyDown(Keys.Right))
            {
                shipRect.X += 50;
            }

            if(newKeyboardState.IsKeyDown(Keys.Space) && newKeyboardState.IsKeyDown(Keys.Left))
            {
                shipRect.X -= 50;
            }

            if(shipRect.X < 0)
            {
                shipRect.X = 0;
            }

            if(shipRect.X > GraphicsDevice.Viewport.Width - shipRect.Width)
            {
                shipRect.X = GraphicsDevice.Viewport.Width - shipRect.Width;
            }

            if (progression > 15) progression = 15;
            meteorRect1.Y += (int)(10 + progression*.9);
            meteorRect2.Y += (int)(10 + progression*1.1);
            meteorRect3.Y += (int)(10 + progression);
            meteorRect4.Y += (int)(10 + progression*.5);
            meteorRect5.Y += (int)(10 + progression*.8);

            if(meteorRect1.Y > 1200)
            {
                meteorRect1.Y = rand.Next(-200, -1);    
                meteorRect1.X = rand.Next(1151);
                progression += 1;
            }
            if(meteorRect2.Y > 1200)
            {
                meteorRect2.Y = rand.Next(-200, -1);    
                meteorRect2.X = rand.Next(1151);
                //progression += 1;
            }
            if(meteorRect3.Y > 1200)
            {
                meteorRect3.Y = rand.Next(-200, -1);
                meteorRect3.X = rand.Next(1151);
                //progression += 1;
            }
            if(meteorRect4.Y > 1200)
            {
                meteorRect4.Y = rand.Next(-200, -1);
                meteorRect4.X = rand.Next(1151);
                //progression += 1;
            }
            if(meteorRect5.Y > 1200)
            {
                meteorRect5.Y = rand.Next(-200, -1);
                meteorRect5.X = rand.Next(1151);
                //progression += 1;
            }

            if (shipRect.Intersects(meteorRect1) || shipRect.Intersects(meteorRect2) || shipRect.Intersects(meteorRect3) || shipRect.Intersects(meteorRect4) || shipRect.Intersects(meteorRect5))
            {
                score += 1;
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
            spriteBatch.Draw(rock, meteorRect1, Color.White);
            spriteBatch.Draw(rock, meteorRect2, Color.White);
            spriteBatch.Draw(rock, meteorRect3, Color.White);
            spriteBatch.Draw(rock, meteorRect4, Color.White);
            spriteBatch.Draw(rock, meteorRect5, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
