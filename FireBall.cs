using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProj
{
    public class FireBall : Sprite
    {
        private float timer;
        public FireBall(Texture2D texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CheckCollision(sprites);

            if (timer > LifeSpan)
                IsRemoved = true;
            Position += Direction * LinearVelocity * 4;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void CheckCollision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if(IsTouching(sprite) &&
                    (sprite is Monster || sprite is Hero) && this.Parent.GetType() != sprite.GetType())
                {
                    IsRemoved = true;
                    if(sprite is Monster)
                        (sprite as Monster).CurrentHealth--;
                    else
                        (sprite as Hero).CurrentHealth--;
                }
            }
        }
    }
}
