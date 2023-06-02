using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameProj
{
    class GameAction
    {
        public static Texture2D BackgroundFieldTexture { get; set; }
        static public SpriteBatch SpriteBatch { get; set; }
        public static int Width, Height;
        public static Random random = new Random();
        public static Texture2D testPic { get; set; }
        static List<Sprite> sprites;
        public static HealthBar healthBar;
        public static Hero player;
        private static TimeSpan timer;
        private static int monstersCount = 0;

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
                    GoUp = Keys.W,
                    GoDown = Keys.S,
                    GoRight = Keys.D,
                    GoLeft = Keys.A,
                    ShootUp = Keys.Up,
                    ShootDown = Keys.Down,
                    ShootRight = Keys.Right,
                    ShootLeft = Keys.Left
                },

                Position = new Vector2(100, 300),

                FireBall = new FireBall(FireBall.fireballTexture),

                StandartAttributes = new Attributes()
                {
                    Speed = 5.5f
                },

                AttributesModifiers = new List<Attributes>()
                {
                    speedModifire
                },

                maxHealth = 4,
                CurrentHealth = 4
                
            };

            healthBar.Player = player;

            sprites = new List<Sprite>()
            {
                player,
            };
            
        }

        public static void Update(GameTime gameTime)
        {
            healthBar.Update();

            if ((gameTime.TotalGameTime - timer > TimeSpan.FromSeconds(3)) || monstersCount == 0)
            {
                timer = gameTime.TotalGameTime;
                if(random.Next(0, 100) >= 75)
                    Monster.GenerateRandomMonster(gameTime, sprites, ref monstersCount);
                Monster.GenerateRandomMonster(gameTime, sprites, ref monstersCount);
            }

            foreach (var sprite in sprites.ToArray())
            {
                sprite.Update(gameTime, sprites);
            }

            PostUpdate();

            if (player.IsRemoved)
                Game1.State = State.ResultAfterGame;
        }

        private static void PostUpdate()
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].IsRemoved)
                {
                    if (sprites[i] is Monster)
                    {
                        monstersCount--;
                        ResultAfterGame.MonstersKilled++;
                    }
                    sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatch.Draw(BackgroundFieldTexture, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
            healthBar.Draw(spriteBatch);
            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);
        }
    }
}
