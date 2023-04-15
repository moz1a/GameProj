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

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    class Hero
    {
        public static Texture2D texture { get; set; }
        Vector2 position;
        Color color = Color.White;
        int speed = 15;
        Direction direction { get; set; } = Direction.Right;
        
        public Hero(Vector2 position)
        {
            this.position = position;
        }

        public void Up() 
        {
            if(IsInBordersOfMap(position, speed, Direction.Up, texture))
                this.position.Y -= speed;
        }
        public void Down()
        {
            if(IsInBordersOfMap(position, speed, Direction.Down, texture))
                this.position.Y += speed;
        }
        public void Right()
        {
            if(IsInBordersOfMap(position, speed, Direction.Right, texture))
                this.position.X += speed;

        }
        public void Left()
        {
            if(IsInBordersOfMap(position, speed, Direction.Left, texture))
                this.position.X -= speed;
        }

        public static bool IsInBordersOfMap(Vector2 position, int speed, Direction direction, Texture2D texture)
        {
            if (direction == Direction.Left && position.X >= -75) return true;
            else if (direction == Direction.Right && position.X <= GameAction.Width - texture.Width + 40) return true;
            else if (direction == Direction.Up && position.Y >= 0) return true;
            else if (direction == Direction.Down && position.Y <= GameAction.Height - texture.Height - 20 ) return true;
            else return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
