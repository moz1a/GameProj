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
            Hero = new Hero(new Vector2(40, 40));
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

    
 }
