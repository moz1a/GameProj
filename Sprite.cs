
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace GameProj
{
    class Sprite
    {
        protected Texture2D texture;
        protected AnimationManager animationManager;
        protected Dictionary<string, Animation> animations;
        protected Vector2 position;

        public Vector2 Position 
        {
            get { return position; }
            set 
            {
                position = value;
                if(animationManager != null) 
                    animationManager.Position = position;
            }
        }
        public Vector2 Velocity;
        public float Speed = 1f;
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

        public Sprite(Dictionary<string, Animation> animations)
        {
            this.animations = animations;
            animationManager = new AnimationManager(this.animations.First().Value);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            PlayAnimations();
            animationManager.Update(gameTime);

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        protected virtual void PlayAnimations()
        {
            if (Velocity.X > 0)
                animationManager.Play(animations["walkRight"]);
            else if (Velocity.X < 0)
                animationManager.Play(animations["walkLeft"]);
            else if (Velocity.Y > 0)
                animationManager.Play(animations["walkDown"]);
            else if (Velocity.Y < 0)
                animationManager.Play(animations["walkUp"]);
        }

        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;

            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Position, Color.White);
            else if (animationManager != null)
                animationManager.Draw(spriteBatch);
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
