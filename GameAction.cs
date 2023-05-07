using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace GameProj
{
    class GameAction
    {
        public static Texture2D BackgroundField { get; set; }
        static public SpriteBatch SpriteBatch { get; set; }
        public static int Width, Height;

        public static Random rnd = new Random();
        public static Texture2D heroSprite { get; set; }
        public static Texture2D monsterSprite { get; set; }

        static List<Sprite> sprites;


        public static void Initialise(SpriteBatch spriteBatch, Dictionary<string, Animation> animations, int width, int height)
        {
            SpriteBatch = spriteBatch;
            Width = width;
            Height = height;

            var speedModifire = new Attributes()
            {
                Speed = 3f,
                HealthPoints = 0
            };

            var player = new Hero(animations)
            {
                Input = new Input()
                {
                    Up = Keys.W,
                    Down = Keys.S,
                    Right = Keys.D,
                    Left = Keys.A
                },
                Position = new Vector2(100, 300),

                StandartAttributes = new Attributes()
                {
                    HealthPoints = 5,
                    Speed = 1f
                },

                AttributesModifiers = new List<Attributes>()
                {
                    speedModifire
                }
            };

            sprites = new List<Sprite>()
            {
                player,


                new Monster(monsterSprite)
                {
                    Position = new Vector2(300, 100),
                    Speed = 5f,
                    FollowTarget = player,
                    FollowDistance = 50
                }
            };
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime, sprites);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatch.Draw(BackgroundField, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);
        }
    }
}
