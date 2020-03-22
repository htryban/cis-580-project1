using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Content.Sprites
{
    public class Ship : Sprite
    {
        public Bullet Bullet;
        
        Game1 game;
        public Ship(Texture2D texture, Game1 game) : base(texture)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, Vector2 dir)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            Direction = dir;
            Position = new Vector2(game.getShipX(), game.getShipY());

            if (currentKey.IsKeyDown(Keys.Space) && previousKey.IsKeyUp(Keys.Space) && game.isStarted())
            {
                var bullet = Bullet.Clone() as Bullet;
                bullet.Direction = this.Direction;
                bullet.Position = this.Position;
                bullet.linearVel = this.linearVel * 4;
                bullet.lifespan = 2f;

                sprites.Add(bullet);
            }

           
        }
    }
}
