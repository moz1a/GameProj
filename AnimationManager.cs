using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GameProj
{
    public class AnimationManager
    {
        private Animation animation;
        private float timer;
        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation)
        {
            this.animation = animation;
        }

        public void Play(Animation animation)
        {
            if (this.animation == animation) return;

            this.animation = animation;
            Stop();
        }

        public void Stop()
        {
            timer = 0f;
            animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > animation.FrameSpeed) 
            {
                timer = 0f;
                animation.CurrentFrame++;

                if (animation.CurrentFrame >= animation.FrameCount)
                    animation.CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation.Texture,
                            Position,
                            new Rectangle(animation.CurrentFrame * animation.FrameWidth,
                                            0,
                                            animation.FrameWidth,
                                            animation.FrameHeight),
                            Color.White);
        }
    }
}
