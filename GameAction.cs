using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace GameProj
{
    class GameAction
    {
        public static Texture2D BackgroundField { get; set; }
        static public SpriteBatch SpriteBatch { get; set; }
        public static int Width, Height;
        public static Random random = new Random();
        public static Texture2D monsterSprite { get; set; }
        public static Texture2D fireballSprite { get; set; }
        public static Texture2D monsterFireballSprite { get; set; }
        public static Texture2D healthPotionSprite { get; set; }

        static List<Sprite> sprites;

        public static HealthBar healthBar;
        public static Hero player;
        private static Monster monster;
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

                FireBall = new FireBall(fireballSprite),

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

            healthBar.player = player;


            sprites = new List<Sprite>()
            {
                player,
            };
            
        }

        private static void GenerateMonsters(GameTime gameTime)
        {
            monster = new Monster(monsterSprite)
            {
                FireBall = new FireBall(monsterFireballSprite),
                Position = SetRandomMonsterPosition(random),
                Speed = 2.5f,
                FollowTarget = player,
                FollowDistance = 0,
                CurrentHealth = 3
            };

            sprites.Add(monster);
            monstersCount++;
        }

        private static Vector2 SetRandomMonsterPosition(Random random)
        {
            return new Vector2(random.Next(0, Width), random.Next(0, Height));
        }

        public static void Update(GameTime gameTime)
        {
            healthBar.Update();

            if ((gameTime.TotalGameTime - timer > TimeSpan.FromSeconds(4)) || monstersCount == 0)
            {
                timer = gameTime.TotalGameTime;
                GenerateMonsters(gameTime);
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
            SpriteBatch.Draw(BackgroundField, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
            
            healthBar.Draw(spriteBatch);
            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);
        }
    }
}
