using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameProj
{
    internal class Monster : Sprite
    {
        private TimeSpan lastTimeDamaged = TimeSpan.Zero;
        public Monster(Texture2D texture) 
            : base(texture)
        {
            Position = new Vector2(500, 100);
        }

        protected void FollowAndCollisionDamage(GameTime gameTime)
        {
            if (FollowTarget == null) return;

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

        private void MakeDelayBeforeNextDamage(float currentDistance, GameTime gameTime)
        {
            if (currentDistance < 50)
            {
                if(gameTime.TotalGameTime - lastTimeDamaged > TimeSpan.FromMilliseconds(300))
                {
                    lastTimeDamaged = gameTime.TotalGameTime;
                    FollowTarget.CurrentHealth--;
                }
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (FollowTarget != null)
                FollowAndCollisionDamage(gameTime);

            base.Update(gameTime, sprites);
        }
    }
}
