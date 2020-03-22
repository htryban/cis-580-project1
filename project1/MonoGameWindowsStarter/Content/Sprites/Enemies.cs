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
    public class Enemies : Sprite
    {
        public Enemy Enemy;
        Random rand = new Random();

        Game1 game;
        public Enemies(Texture2D texture, Game1 game) : base(texture)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, Vector2 dir)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            Direction = dir;
            //Position = new Vector2(game.getShipX(), game.getShipY());
            Position = new Vector2((float)rand.Next(50, 1750), (float)rand.Next(50, 1250));

            if (currentKey.IsKeyDown(Keys.R) && previousKey.IsKeyUp(Keys.R) && game.isStarted())
            {
                var enemy = Enemy.Clone() as Enemy;
                enemy.Direction = new Vector2();
                enemy.Position = this.Position;
                enemy.linearVel = this.linearVel * 3;
               
                sprites.Add(enemy);
            }


        }
    }
}
