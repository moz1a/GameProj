using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProj
{
    public static class ResultAfterGame
    {
        public static Texture2D Background;
        public static SpriteFont Font;
        private static Rectangle texturePosition = new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight);
        private static Vector2 textPosition = new Vector2(Game1.ScreenWidth / 16, Game1.ScreenHeight * 0.8f);
        public static int MonstersKilled = 0;
        private static int timeCounter = 0;
        private static Color color;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, texturePosition, Color.White);
            spriteBatch.DrawString(Font, $"Monsters killed: {MonstersKilled}", textPosition, color);
            spriteBatch.DrawString(Font, $"Press Enter to die again...", textPosition + new Vector2(0, 100), color);
        }

        public static void Update()
        {
            color = Color.FromNonPremultiplied(187, 0, 30, timeCounter % 400);
            timeCounter++;
        }
    }
}
