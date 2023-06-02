using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace GameProj
{
    public class Hero : Sprite
    {
        public Attributes StandartAttributes { get; set; }
        public List<Attributes> AttributesModifiers { get; set; }
        public Attributes TotalAttributes
        {
            get
            {
                return StandartAttributes + AttributesModifiers.Sum();
            }
        }
        public int maxHealth;
        public int CurrentHealth;
        private TimeSpan lastTimeShooted;
        public FireBall FireBall;


        public Hero(Dictionary<string, Animation> animations) 
            : base(animations)
        {
            StandartAttributes = new Attributes();
            AttributesModifiers = new List<Attributes>(); 
        }
        public Hero(Texture2D texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            Speed = TotalAttributes.Speed;
            Move();

            if((previousKey.IsKeyUp(Input.ShootLeft) && currentKey.IsKeyDown(Input.ShootLeft)) ||
              (previousKey.IsKeyUp(Input.ShootRight) && currentKey.IsKeyDown(Input.ShootRight)) ||
              (previousKey.IsKeyUp(Input.ShootUp) && currentKey.IsKeyDown(Input.ShootUp)) ||
              (previousKey.IsKeyUp(Input.ShootDown) && currentKey.IsKeyDown(Input.ShootDown)))
            {
                if(IsReloaded(gameTime))
                    AddFireball(sprites);
            }

            if (CurrentHealth <= 0)
                this.IsRemoved = true;

            base.Update(gameTime, sprites);
        }

        private bool IsReloaded(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - lastTimeShooted > TimeSpan.FromMilliseconds(400))
            {
                lastTimeShooted = gameTime.TotalGameTime;
                return true;
            }
            return false;
        }

        private void AddFireball(List<Sprite> sprites)
        {
            var fireball = FireBall.Clone() as FireBall;
            fireball.Direction = GetDirectionForShoot();
            fireball.Position = this.Position;
            SetVelocityFireball(fireball);
            fireball.LifeSpan = 2f;
            fireball.Parent = this;

            sprites.Add(fireball);
        }

        private void SetVelocityFireball(FireBall fireBall)
        {
            if (Keyboard.GetState().IsKeyDown(Input.ShootRight) && Keyboard.GetState().IsKeyDown(Input.GoRight))
                fireBall.LinearVelocity = this.LinearVelocity * 1.6f;
            else if (Keyboard.GetState().IsKeyDown(Input.ShootLeft) && Keyboard.GetState().IsKeyDown(Input.GoLeft))
                fireBall.LinearVelocity = this.LinearVelocity * 1.6f;
            else if (Keyboard.GetState().IsKeyDown(Input.ShootDown) && Keyboard.GetState().IsKeyDown(Input.GoDown))
                fireBall.LinearVelocity = this.LinearVelocity * 1.6f;
            else if (Keyboard.GetState().IsKeyDown(Input.ShootUp) && Keyboard.GetState().IsKeyDown(Input.GoUp))
                fireBall.LinearVelocity = this.LinearVelocity * 1.6f;
            else
                fireBall.LinearVelocity = this.LinearVelocity;
        }

        private Vector2 GetDirectionForShoot()
        {
            if (Keyboard.GetState().IsKeyDown(Input.ShootLeft))
                return new Vector2(-1, 0);
            if (Keyboard.GetState().IsKeyDown(Input.ShootRight))
                return new Vector2(1, 0);
            if (Keyboard.GetState().IsKeyDown(Input.ShootUp))
                return new Vector2(0, -1);
            if (Keyboard.GetState().IsKeyDown(Input.ShootDown))
                return new Vector2(0, 1);

            else throw new Exception("Invalid direction for shoot");
        }
            
        void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.GoLeft))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.GoRight))
                Velocity.X = Speed;

            if (Keyboard.GetState().IsKeyDown(Input.GoUp))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.GoDown))
                Velocity.Y = Speed;

            Position = Vector2.Clamp(Position, new Vector2(0, 0),
                new Vector2(Game1.ScreenWidth - this.Rectangle.Width, Game1.ScreenHeight - this.Rectangle.Height));
        }
    }
}
