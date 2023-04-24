using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProj
{
    static class Menu
    {
        public static Texture2D Background { get; set; }
        public static SpriteFont Font { get; set; }
        private static int timeCounter = 0;
        private static Color color;
        private static Rectangle texturePosition = new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight);
        private static Vector2 textPosition = new Vector2(Game1.ScreenWidth/16, 50);

        public static void Update()
        {
            color = Color.FromNonPremultiplied(187, 0, 0, timeCounter % 400);
            timeCounter++;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, texturePosition, Color.White);
            spriteBatch.DrawString(Font, "Oblivion liberty", textPosition, color);

        }
    }
}
