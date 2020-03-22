using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
using MonoGameWindowsStarter.Content.Sprites;

namespace MonoGameWindowsStarter
{
    enum shipMovement
    {
        Idle,
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        int progression = 0;
        int score = 0;
        int eat = 0;
        int displayScore = 0;
        string te = "";
        bool scored = false;
        bool died = false;
        bool safe = false;
        bool _meteorsStarted = false;
        bool _draw = true;
        bool _paused = true;
        bool _started = false;
        float rotation = 0;
        float _timer = 15;
        SpriteEffects effect = SpriteEffects.None;

        private SpriteFont _font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Song backingtrack;
        List<SoundEffect> soundEffects;
        Player player;

        Random rand = new Random();
        Rectangle shipRect;
        Texture2D ship;
        Texture2D rock;
        Texture2D bull;
        KeyboardState newKeyboardState;
        KeyboardState prevKey;
        Meteor m1;
        shipMovement movement = shipMovement.Idle;

        private List<Sprite> _sprite;
        private List<Sprite> _enemySprite;
        Vector2 direction = new Vector2(0, -1);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            soundEffects = new List<SoundEffect>();
            player = new Player(this);
            m1 = new Meteor { meteorRect = new Rectangle(0,0, 50,50), Progression = 1.0, Width = 50, Height = 50 };
            //flyweight = new Flyweight(new Meteor { meteorRect = meteorRect1, Progression = 1.0, Width = 50, Height = 50 });
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1800;
            graphics.PreferredBackBufferHeight = 1300;
            graphics.ApplyChanges();

            shipRect.X = 900;
            shipRect.Y = 900;
            shipRect.Width = 70;
            shipRect.Height = 80;

            _font = Content.Load<SpriteFont>("testfont"); 

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ship = Content.Load<Texture2D>("blackship");
            rock = Content.Load<Texture2D>("meteor");
            bull = Content.Load<Texture2D>("Bullet");
                
            this.backingtrack = Content.Load<Song>("Hotshot");
            //MediaPlayer.Play(backingtrack);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            soundEffects.Add(Content.Load<SoundEffect>("boom"));
            soundEffects.Add(Content.Load<SoundEffect>("ship jump blip"));
            player.LoadContent();

            _sprite = new List<Sprite>()
            {
                new Ship(ship, this)
                {
                    Position = new Vector2(shipRect.X, shipRect.Y),
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet")),
                },
            };
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(backingtrack);
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
            if(_started) _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            prevKey = newKeyboardState;
            newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();
            if (eat < 2 && newKeyboardState.IsKeyDown(Keys.Space))
            {
                _started = true;
                _draw = true;
                if(died)
                {
                    shipRect.X = 900;
                    shipRect.Y = 900;
                    rotation = 0;
                    direction = new Vector2(0, -1);
                    _timer = 15;
                }

                //eats the first space to prevent firing on start
                eat += 1;
            }
            
            if(_started){
                if (newKeyboardState.IsKeyDown(Keys.R) && !_meteorsStarted && prevKey.IsKeyDown(Keys.R))
                {
                    _meteorsStarted = true;
                    _enemySprite = new List<Sprite>()
                    {
                        new Enemies(rock, this)
                        {
                            Position = new Vector2(100, 100),
                            Enemy = new Enemy(Content.Load<Texture2D>("Meteor"))
                        }
                        
                    };
                }

                

                if ((newKeyboardState.IsKeyDown(Keys.Up) && (newKeyboardState.IsKeyDown(Keys.Left))) || (newKeyboardState.IsKeyDown(Keys.W) && (newKeyboardState.IsKeyDown(Keys.A)))) movement = shipMovement.UpLeft;
                else if((newKeyboardState.IsKeyDown(Keys.Up) && (newKeyboardState.IsKeyDown(Keys.Right))) || (newKeyboardState.IsKeyDown(Keys.W) && (newKeyboardState.IsKeyDown(Keys.D)))) movement = shipMovement.UpRight;
                else if((newKeyboardState.IsKeyDown(Keys.Down) && (newKeyboardState.IsKeyDown(Keys.Left))) || (newKeyboardState.IsKeyDown(Keys.S) && (newKeyboardState.IsKeyDown(Keys.A)))) movement = shipMovement.DownLeft;
                else if((newKeyboardState.IsKeyDown(Keys.Down) && (newKeyboardState.IsKeyDown(Keys.Right))) || (newKeyboardState.IsKeyDown(Keys.S) && (newKeyboardState.IsKeyDown(Keys.D)))) movement = shipMovement.DownRight;
                else if (newKeyboardState.IsKeyDown(Keys.Up) || newKeyboardState.IsKeyDown(Keys.W)) movement = shipMovement.Up;
                else if (newKeyboardState.IsKeyDown(Keys.Down) || newKeyboardState.IsKeyDown(Keys.S)) movement = shipMovement.Down;
                else if (newKeyboardState.IsKeyDown(Keys.Left) || newKeyboardState.IsKeyDown(Keys.A)) movement = shipMovement.Left;
                else if (newKeyboardState.IsKeyDown(Keys.Right) || newKeyboardState.IsKeyDown(Keys.D)) movement = shipMovement.Right;
                else movement = shipMovement.Idle;
            }

            if(_started && _draw)
            {
                switch(movement)
                {
                    case shipMovement.UpLeft:
                        shipRect.Y -= 7;
                        shipRect.X -= 7;
                        rotation = (float)-.5;
                        direction = new Vector2(-1, -1);
                        break;
                    case shipMovement.UpRight:
                        shipRect.Y -= 7;
                        shipRect.X += 7;
                        rotation = (float).5;
                        direction = new Vector2(1, -1);
                        break;
                    case shipMovement.DownRight:
                        shipRect.Y += 7;
                        shipRect.X += 7;
                        rotation = (float)2.2;
                        direction = new Vector2(1, 1);
                        break;
                    case shipMovement.DownLeft:
                        shipRect.Y += 7;
                        shipRect.X -= 7;
                        rotation = (float)-2.2;
                        direction = new Vector2(-1, 1);
                        break;
                    case shipMovement.Up:
                        shipRect.Y -= 10;
                        rotation = 0;
                        effect = SpriteEffects.None;
                        direction = new Vector2(0, -1);
                        break;
                    case shipMovement.Down:
                        shipRect.Y += 10;
                        rotation = (float)3.125;
                        direction = new Vector2(0, 1);
                        break;
                    case shipMovement.Left:
                        shipRect.X -= 10;
                        rotation = (float)-1.6;
                        direction = new Vector2(-1, 0);
                        break;
                    case shipMovement.Right:
                        shipRect.X += 10;
                        rotation = (float)1.6;
                        direction = new Vector2(1,0);
                        break;
                    case shipMovement.Idle:
                    default: break;
                }
            }
            
            //keep ship on screen
            if (shipRect.X < 0 + (shipRect.Width / 2)) shipRect.X = 0 + (shipRect.Width / 2);
            
            if (shipRect.X > GraphicsDevice.Viewport.Width - (shipRect.Width / 2)) shipRect.X = GraphicsDevice.Viewport.Width - (shipRect.Width / 2);
            
            if (shipRect.Y < 0  + (shipRect.Height / 2)) shipRect.Y = 0 + (shipRect.Height / 2);

            if (shipRect.Y > GraphicsDevice.Viewport.Height - (shipRect.Height / 2)) shipRect.Y = GraphicsDevice.Viewport.Height - (shipRect.Height / 2);
          
            

            foreach (var sprite in _sprite.ToArray())
                sprite.Update(gameTime, _sprite, direction, 0);

            if (_enemySprite != null) {
                foreach (var sprite in _enemySprite.ToArray())
                    sprite.Update(gameTime, _enemySprite, direction, 5);
            }

            if (_timer <= 0) 
            {
                for (int i = 1; i < _enemySprite.Count; i++) _enemySprite.RemoveAt(i);
                died = true;
                _draw = false;
                _started = false;
                var instance = soundEffects[0].CreateInstance();
                instance.Play();
                eat = 0;
                //shipRect.X = 900;
                //shipRect.Y = 900;
                displayScore = score;
                score = 0;
            }

            if (_enemySprite != null) {
                safe = false;
                for (int e = 1; e < _enemySprite.Count; e++)
                {
                    scored = false;
                    for (int i = 1; i < _sprite.Count; i++)
                    {
                        try {
                            if(_enemySprite[e] == null || _sprite[i] == null)
                            {
                                continue;
                            }
                            else if(_sprite[i].isRemoved)
                            {
                                _sprite.RemoveAt(i);
                                i--;
                            }
                            else if (colliding(_enemySprite[e].Position, _sprite[i].Position))
                            {
                                if (!scored)
                                {
                                    scored = true;
                                    score++;
                                    _enemySprite.RemoveAt(e);
                                    _sprite.RemoveAt(i);
                                }
                            }
                        }
                        catch (ArgumentOutOfRangeException oor)
                        {
                            continue;
                        }
                        if (scored) continue;
                    }
                    try {
                        if(shipRect.Intersects(toRect(_enemySprite[e].Position)))
                        {
                            for(int i = 1; i < _enemySprite.Count; i++) _enemySprite.RemoveAt(i);
                            died = true;
                            _draw = false;
                            _started = false;
                            var instance = soundEffects[0].CreateInstance();
                            instance.Play();
                            eat = 0;
                            //shipRect.X = 900;
                            //shipRect.Y = 900;
                            displayScore = score;
                            score = 0;
                            break;
                        }
                    }
                    catch (ArgumentOutOfRangeException oor)
                    {
                        continue;
                    }
                }
                safe = true;
            }

            player.Update(gameTime);

            base.Update(gameTime);
        }

        public bool colliding(Vector2 enemy, Vector2 bullet)
        {
            if ((bullet.X >= enemy.X - 20) && (bullet.X <= enemy.X + 28) && (bullet.Y <= enemy.Y + 40) && (bullet.Y + bull.Height + 20 >= enemy.Y)) return true;
            return false;
        }

        public Rectangle toRect(Vector2 a)
        {
            return new Rectangle((int)a.X + 5, (int)a.Y, 40, 50);
        }

        public bool isStarted()
        {
            if (eat > 1) return true;
            return false;
        }

        public float getShipX()
        {
            return shipRect.X;
        }

        public float getShipY()
        {
            return shipRect.Y;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            if(_draw) spriteBatch.Draw(ship, shipRect, null, Color.White, rotation, new Vector2(ship.Width / 2f, ship.Height / 2f), effect, 0);
            
            for (int i = 1; i < _sprite.Count; i++)
                _sprite[i].Draw(spriteBatch);
            if (_enemySprite != null)
            {
                for (int i = 1; i < _enemySprite.Count; i++)
                    _enemySprite[i].Draw(spriteBatch);
            }
                        
            if(_started)spriteBatch.DrawString(_font, "Score: " + score + "\nPress 'R' to spawn the next wave of enemies ", new Vector2(0,0), Color.White);
            if (_started) spriteBatch.DrawString(_font, "Time Left: " + _timer, new Vector2(1600, 0), Color.White);
            if (!_started && !died) spriteBatch.DrawString(_font,
                "                       Use the Arrows or WASD to Move\n                        Press Space to Shoot / Continue\n                        Destroy as many Meteors you can in the given time",
                new Vector2(600,650), Color.White);
            if(died && !_started){ spriteBatch.DrawString(_font, 
                "                       You Destroyed " + displayScore + " Meteors\n                       Press Space to play again", new Vector2(600,650), Color.White);
                player.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
