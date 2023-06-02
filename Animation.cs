using Microsoft.Xna.Framework.Graphics;

namespace GameProj
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public int FrameHeight { get { return Texture.Height; } }
        public float FrameSpeed { get; set; }
        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount) 
        {
            Texture = texture;
            FrameCount = frameCount;
            FrameSpeed = 0.15f;
        }
    }
}
