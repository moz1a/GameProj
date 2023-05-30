using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameProj
{
    class GameAction
    {
        public static Texture2D BackgroundField { get; set; }
        static public SpriteBatch SpriteBatch { get; set; }
        public static int Width, Height;
        public static Random random = new Random();
        public static Texture2D slugSprite { get; set; }
        public static Texture2D skeletonSprite { get; set; }
        public static Texture2D wizardSprite { get; set; }
        public static Texture2D fireballSprite { get; set; }
        public static Texture2D monsterFireballSprite { get; set; }
        public static Texture2D healthPotionSprite { get; set; }

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

            healthBar.Player = player;

            sprites = new List<Sprite>()
            {
                player,
            };
            
        }

        private static void GenerateMonster(GameTime gameTime)
        {
            var chanceToSpawn = random.Next(0, 100);
            if (chanceToSpawn <= 33)
                sprites.Add(CreateWizard());
            else if (chanceToSpawn > 33 && chanceToSpawn <= 66)
                sprites.Add(CreateSlug());
            else
                sprites.Add(CreateSkeleton());

            monstersCount++;
        }

        private static Monster CreateSkeleton()
        {
            return new Monster(skeletonSprite)
            {
                Position = SetRandomMonsterPosition(random),
                Speed = 2.3f,
                FollowTarget = player,
                DistanceToApproximate = 0,
                CurrentHealth = 7,
            };
        }

        private static Monster CreateSlug()
        {
            var slug = new Monster(slugSprite)
            {
                FireBall = new FireBall(monsterFireballSprite),
                Position = SetRandomMonsterPosition(random),
                Speed = 2f,
                FollowTarget = player,
                DistanceToApproximate = 0,
                CurrentHealth = 4,
            };
            slug.Shooting = slug.MakeDefaultDirectionToShot;
            return slug;
        }

        private static Monster CreateWizard()
        {
            var wizard = new Monster(wizardSprite)
            {
                FireBall = new FireBall(monsterFireballSprite),
                Position = SetRandomMonsterPosition(random),
                Speed = 1.5f,
                FollowTarget = player,
                DistanceToApproximate = 300,
                CurrentHealth = 3,
            };
            wizard.Shooting = wizard.MakeTripleShot;
            return wizard;
        }

        private static Vector2 SetRandomMonsterPosition(Random random)
        {
            while (true)
            {
                var position = new Vector2(random.Next(0, Width), random.Next(0, Height));
                if (Vector2.Distance(position, player.Position) > 200)
                    return position;
            }
        }

        public static void Update(GameTime gameTime)
        {
            healthBar.Update();

            if ((gameTime.TotalGameTime - timer > TimeSpan.FromSeconds(3)) || monstersCount == 0)
            {
                timer = gameTime.TotalGameTime;
                GenerateMonster(gameTime);
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
