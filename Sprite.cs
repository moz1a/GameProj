
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProj
{
    class Sprite
    {
        private Texture2D texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Speed;
        public Input Input;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Left &&
                   this.Rectangle.Bottom > sprite.Rectangle.Top && 
                   this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                   this.Rectangle.Right > sprite.Rectangle.Right &&
                   this.Rectangle.Bottom > sprite.Rectangle.Top &&
                   this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                   this.Rectangle.Top < sprite.Rectangle.Top &&
                   this.Rectangle.Right > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
                   this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                   this.Rectangle.Right > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Right;
        }
        #endregion
    }
}
