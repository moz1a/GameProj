
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProj
{
    public class Sprite : ICloneable
    {
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;


        public Vector2 Velocity;
        public float LinearVelocity = 1f;
        public float Speed = 0.1f;
        public Input Input;
        private Texture2D texture;
        private AnimationManager animationManager;
        private Dictionary<string, Animation> animations;
        private Vector2 position;
        private Vector2 origin;
        public Hero FollowTarget { get; set; }
        public float FollowDistance { get; set; }
        public bool IsRemoved = false;
        public float LifeSpan = 0f;
        public Sprite Parent;
        public Vector2 Direction;
        protected float rotation { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Vector2 Position 
        {
            get { return position; }
            set 
            {
                if(texture != null)
                    position = value + new Vector2(texture.Width/2, texture.Height/2);
                position = value;
                if (animationManager != null) 
                    animationManager.Position = position;
            }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set
            { 
                origin = value;
            }
        }
        
        public Rectangle Rectangle
        {
            get
            {
                if(texture != null)
                    return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
                return new Rectangle((int)Position.X, (int)Position.Y, animationManager.Animation.FrameWidth,
                    animationManager.Animation.FrameHeight);
            }
            set { }
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            this.animations = animations;
            animationManager = new AnimationManager(this.animations.First().Value);
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, animationManager.Animation.Texture.Width,
                animationManager.Animation.Texture.Height);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            CheckCollision(sprites);
            if (animationManager != null)
            {
                PlayAnimations();
                animationManager.Update(gameTime);
            }
           
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Position, Color.White);
            else if (animationManager != null)
                animationManager.Draw(spriteBatch);
        }

        #region Collision
        public virtual void CheckCollision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite)
                 || this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                {
                    this.Velocity.X = 0;
;
                }

                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite)
                 || this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                {
                    this.Velocity.Y = 0;
                }
            }
        }

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
