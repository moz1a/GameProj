using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProj
{
    public class AnimationManager
    {
        public Animation Animation { get; set; }
        private float timer;
        public Vector2 Position { get; set; }
        private Vector2 previousPosition;

        public AnimationManager(Animation animation)
        {
            this.Animation = animation;
        }

        public void Play(Animation animation)
        {
            if (this.Animation == animation) return;
            
            Animation = animation;
            
            Stop();
        }

        public void Stop()
        {
            timer = 0f;
            Animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (previousPosition == Position)
                return;
            previousPosition = Position;

            if (timer > Animation.FrameSpeed)
            {
                timer = 0f;
                Animation.CurrentFrame++;

                if (Animation.CurrentFrame >= Animation.FrameCount)
                {
                    Animation.CurrentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Animation.Texture,
                            Position,
                            new Rectangle(Animation.CurrentFrame * Animation.FrameWidth,
                                            0,
                                            Animation.FrameWidth,
                                            Animation.FrameHeight),
                            Color.White);
        }
    }
}
