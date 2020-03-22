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
    public class Enemy : Sprite
    {

        public Enemy(Texture2D texture) : base(texture)
        {

        }
        public override void Update(GameTime gameTime, List<Sprite> sprites, Vector2 dir, int count)
        {
            Position += Direction * linearVel;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures, Position, null, Color.White, _rotation, Origin, .2f, SpriteEffects.None, 0);
        }
    }
}
