
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        protected Vector2 origin;
        public Vector2 Direction { get; private set; }
        protected float rotation { get; private set; }

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

        public Vector2 Origin
        {
            get { return origin; }
            set
            { 
                origin = value;
            }
        }

        public Vector2 Velocity;
        public float LinearVelocity = 1f;
        public float Speed = 0.1f;
        public Input Input;
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

        public Sprite FollowTarget {  get; set; }
        public float FollowDistance { get; set; }
        public bool IsRemoved { get; set; }
          

        public Sprite(Dictionary<string, Animation> animations)
        {
            this.animations = animations;
            animationManager = new AnimationManager(this.animations.First().Value);
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, animationManager.Animation.Texture.Width,
                animationManager.Animation.Texture.Height);
        }

        protected void Follow()
        {
            if (FollowTarget == null) return;

            var distance = FollowTarget.Position - this.Position;
            rotation = (float)Math.Atan2(distance.Y, distance.X);
            Direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

            var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);

            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min((float)Math.Abs(currentDistance - FollowDistance), LinearVelocity);
                var velocity = Direction * t;
                Velocity = velocity;
            }
            //Position += Velocity;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (FollowTarget != null)
                Follow();
            else
                Move();
            CheckCollision(sprites);
            if (animationManager != null)
            {
                PlayAnimations();
                animationManager.Update(gameTime);
            }
            

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        private void CheckCollision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite)
                || this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = 0;

                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite)
                 || this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = 0;
            }
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

            Position = Vector2.Clamp(Position, new Vector2(0, 0),
                new Vector2(Game1.ScreenWidth - this.Rectangle.Width, Game1.ScreenHeight - this.Rectangle.Height));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Position, null, Color.White, rotation, Origin, 1f, SpriteEffects.None, 0);
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
