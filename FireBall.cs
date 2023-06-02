using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameProj
{
    public class FireBall : Sprite
    {
        public static Texture2D fireballTexture { get; set; }
        private float timer;
        public FireBall(Texture2D texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            DontRunOnOther(sprites);

            if (timer > LifeSpan)
                IsRemoved = true;
            Position += Direction * LinearVelocity * 4;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void DontRunOnOther(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (CheckRectangleCollision(sprite))
                {
                    var spriteTexture = sprite is Hero ? (sprite as Hero).animationManager.Animation.Texture : sprite.Texture;

                    if ((sprite is Monster || sprite is Hero) && this.Parent.GetType() != sprite.GetType() &&
                        CheckPerPixelCollision(this.Rectangle, this.Texture, sprite.Rectangle, spriteTexture))
                    {
                        IsRemoved = true;
                        if (sprite is Monster)
                            (sprite as Monster).CurrentHealth--;
                        else
                            (sprite as Hero).CurrentHealth--;
                    }
                }
            }
        }
    }
}
