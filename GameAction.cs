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
        public static Texture2D monsterSprite { get; set; }
        public static Texture2D fireballSprite { get; set; }
        public static Texture2D monsterFireballSprite { get; set; }

        static List<Sprite> sprites;

        public static HealthBar healthBar;
        public static Hero player;

        public static void Initialise(SpriteBatch spriteBatch, Dictionary<string, Animation> animations, int width, int height)
        {
            SpriteBatch = spriteBatch;
            Width = width;
            Height = height;

            

            var speedModifire = new Attributes()
            {
                Speed = 1.5f,
            };

            player = new Hero(animations)
            {
                Input = new Input()
                {
                    Up = Keys.W,
                    Down = Keys.S,
                    Right = Keys.D,
                    Left = Keys.A,
                    ShootUp = Keys.Up,
                    ShootDown = Keys.Down,
                    ShootRight = Keys.Right,
                    ShootLeft = Keys.Left
                },

                Position = new Vector2(100, 300),

                FireBall = new FireBall(fireballSprite),

                StandartAttributes = new Attributes()
                {
                    Speed = 3f
                },

                AttributesModifiers = new List<Attributes>()
                {
                    speedModifire
                },

                maxHP = 4,
                CurrentHealth = 4
                
            };

            healthBar.player = player;

            var monster = new Monster(monsterSprite)
            {
                FireBall = new FireBall(monsterFireballSprite),
                Position = new Vector2(300, 100),
                Speed = 2.5f,
                FollowTarget = player,
                FollowDistance = 0,
                CurrentHealth = 3
            };


            sprites = new List<Sprite>()
            {
                player,
                monster,



            };
        }

        public static void Update(GameTime gameTime)
        {
            healthBar.Update();
            foreach (var sprite in sprites.ToArray())
            {
                sprite.Update(gameTime, sprites);
            }

            PostUpdate();

            if (player.IsRemoved)
            {
                ResultAfterGame.MonstersKilled = 5;
                Game1.state = State.ResultAfterGame;
            }
        }

        private static void PostUpdate()
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].IsRemoved)
                {
                    sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatch.Draw(BackgroundField, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
            healthBar.Draw(spriteBatch);
            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);

        }
    }
}
