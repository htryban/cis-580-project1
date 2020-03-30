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
        float _timer = 0;

        Game1 game;
        public Enemies(Texture2D texture, Game1 game) : base(texture)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites, Vector2 dir, int count)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            Direction = dir;
            //Position = new Vector2(game.getShipX(), game.getShipY());
            

            if (true)//previousKey.IsKeyUp(Keys.P) && game.isStarted())
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_timer >= 3)
                {
                    _timer -= 3;
                    for (int i = 0; i < count; i++)
                    {
                        Position = new Vector2((float)rand.Next(50, 1750), (float)rand.Next(50, 1250));
                        spawnEnemies(sprites);
                    }
                }
            } 


        }

        private void spawnEnemies(List<Sprite> sprites)
        {
                var enemy = Enemy.Clone() as Enemy;
                enemy.Direction = new Vector2();
                enemy.Position = this.Position;
                enemy.linearVel = this.linearVel * 2;

                sprites.Add(enemy);
           
        }

    }
}
