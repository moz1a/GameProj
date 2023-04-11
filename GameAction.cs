using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.CompilerServices;

namespace GameProj
{
    class GameAction
    {
        public static Texture2D BackgroundField { get; set; }
        static public SpriteBatch SpriteBatch { get; set; }
        public static int Width, Height;
        public static Random rnd = new Random();
        static public Hero Hero { get; set; }

        public static void Initialise(SpriteBatch spriteBatch, int width, int height)
        {
            SpriteBatch = spriteBatch;
            Width = width;
            Height = height;
            Hero = new Hero(new Vector2(0, 0));
        }

        public static void Update()
        {
        }

        public static void Draw()
        {
            SpriteBatch.Draw(BackgroundField, new Rectangle(0, 0, 1920, 1080), Color.White);
            Hero.Draw(SpriteBatch);
        }
    }



    class Hero
    {
        public static Texture2D texture { get; set; }
        Vector2 position;
        Color color = Color.White;
        int Speed = 5;
        
        public Hero(Vector2 position)
        {
            this.position = position;
        }

        public void Up() 
        {
            this.position.Y -= Speed;
        }
        public void Down()
        {
            this.position.Y += Speed;
        }
        public void Right()
        {
            this.position.X += Speed;
        }
        public void Left()
        {
            this.position.X -= Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
