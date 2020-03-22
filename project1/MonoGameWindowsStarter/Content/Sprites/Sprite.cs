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
    public class Sprite : ICloneable
    {
        protected Texture2D _textures;
        protected float _rotation;
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;

        public Vector2 Position;
        public Vector2 Origin;

        public Vector2 Direction;
        public float rotateVel = 3f;
        public float linearVel = 4f;

        public float lifespan = 0f;

        public bool isRemoved = false;

        public Sprite(Texture2D texture)
        {
            _textures = texture;
            Origin = new Vector2(_textures.Width / 2, _textures.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites, Vector2 dir)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
