using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameProj
{
    internal class Monster : Sprite
    {
        private TimeSpan lastTimeDamagedPlayer = TimeSpan.Zero;
        private TimeSpan lastTimeShooted = TimeSpan.Zero;
        public int CurrentHealth;
        public FireBall FireBall;

        public Monster(Texture2D texture)
            : base(texture)
        {
            Position = new Vector2(1500, 400);
        }

        protected void FollowAndCollisionDamage(GameTime gameTime)
        {
            if (FollowTarget == null) return;
            LinearVelocity = Speed;
            var distance = FollowTarget.Position - this.Position;
            rotation = (float)Math.Atan2(distance.Y, distance.X);
            Direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

            var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);

            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min((float)Math.Abs(currentDistance), LinearVelocity);
                var velocity = Direction * t;
                Velocity = velocity;
            }

            MakeDelayBeforeNextDamage(currentDistance, gameTime);

        }
        private void ShootMonster(List<Sprite> sprites)
        {
            var fireball = FireBall.Clone() as FireBall;
            fireball.Direction = this.Direction;
            fireball.Position = this.Position;
            fireball.LinearVelocity = Speed * 0.6f;
            fireball.LifeSpan = 7f;
            fireball.Parent = this;

            sprites.Add(fireball);
        }

        private void MakeDelayBeforeNextDamage(float currentDistance, GameTime gameTime)
        {
            if (currentDistance < 55)
            {
                if (gameTime.TotalGameTime - lastTimeDamagedPlayer > TimeSpan.FromMilliseconds(200))
                {
                    lastTimeDamagedPlayer = gameTime.TotalGameTime;
                    FollowTarget.CurrentHealth--;
                }
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (FollowTarget != null)
                FollowAndCollisionDamage(gameTime);

            if (gameTime.TotalGameTime - lastTimeShooted > TimeSpan.FromMilliseconds(1000))
            {
                lastTimeShooted = gameTime.TotalGameTime;
                ShootMonster(sprites);
            }

            if (CurrentHealth <= 0) IsRemoved = true;
            base.Update(gameTime, sprites);
        }
    }
}
