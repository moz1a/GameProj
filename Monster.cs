using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProj
{
    public class Monster : Sprite
    {
        public static Texture2D slugTexture { get; set; }
        public static Texture2D skeletonTexture { get; set; }
        public static Texture2D wizardTexture { get; set; }
        public static Texture2D monsterFireballTexture { get; set; }

        private TimeSpan lastTimeDamagedPlayer = TimeSpan.Zero;
        private TimeSpan lastTimeShooted = TimeSpan.Zero;
        public int CurrentHealth;
        public FireBall FireBall;
        private static Random random = new Random();
        public Func<List<Vector2>> Shooting;
        private float currentDistanceToPlayer;
        public Monster(Texture2D texture)
            : base(texture)
        {
        }

        public static void GenerateRandomMonster(GameTime gameTime, List<Sprite> sprites, ref int monstersCount)
        {
            var chanceToSpawn = random.Next(0, 100);
            if (chanceToSpawn <= 25)
                sprites.Add(CreateWizard());
            else if (chanceToSpawn > 25 && chanceToSpawn <= 62)
                sprites.Add(CreateSlug());
            else
                sprites.Add(CreateSkeleton());

            monstersCount++;
        }

        private void FollowAndCollisionDamage(GameTime gameTime)
        {
            if (FollowTarget == null) return;
            LinearVelocity = Speed;
            var distance = FollowTarget.Position - this.Position;
            rotation = (float)Math.Atan2(distance.Y, distance.X);
            Direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

            currentDistanceToPlayer = Vector2.Distance(this.Position, FollowTarget.Position);

            if (currentDistanceToPlayer > DistanceToApproximate)
            {
                var t = MathHelper.Min((float)Math.Abs(currentDistanceToPlayer), LinearVelocity);
                var velocity = Direction * t;
                Velocity = velocity;
            }
            MakeDelayBeforeNextDamage(currentDistanceToPlayer, gameTime);
        }

        private void ShootMonster(List<Sprite> sprites)
        {
            if(currentDistanceToPlayer < 1000)
                GenerateShot(sprites, Shooting);
        }

        public List<Vector2> MakeDefaultDirectionToShot()
        {
            return new List<Vector2>() { Direction };
        }

        public List<Vector2> MakeTripleShot()
        {
            var dir1 = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            var dir2 = new Vector2((float)Math.Cos(rotation + Math.PI/7), (float)Math.Sin(rotation + Math.PI / 7));
            var dir3 = new Vector2((float)Math.Cos(rotation - Math.PI / 7), (float)Math.Sin(rotation - Math.PI / 7));
            return new List<Vector2>() { dir1, dir2, dir3 };
        }

        private void GenerateShot(List<Sprite> sprites, Func<List<Vector2>> MakeDirectionToShot)
        {
            if (MakeDirectionToShot.Invoke().Count == 1)
            {
                FireBall fireball = MakeFireballWithoutDirection();
                fireball.Direction = MakeDirectionToShot.Invoke().First();
                sprites.Add(fireball);
            }
                
            else
            {
                foreach(var direction in MakeDirectionToShot.Invoke())
                {
                    FireBall fireball = MakeFireballWithoutDirection();
                    fireball.Direction = direction;
                    sprites.Add(fireball);
                }
            }
        }

        private FireBall MakeFireballWithoutDirection()
        {
            var fireball = FireBall.Clone() as FireBall;
            fireball.Position = Position + new Vector2(this.Texture.Width / 4 + this.Texture.Height / 4);
            fireball.LinearVelocity = Speed * 0.6f;
            fireball.LifeSpan = 7f;
            fireball.Parent = this;
            return fireball;
        }

        private void MakeDelayBeforeNextDamage(float currentDistance, GameTime gameTime)
        {
            if (currentDistance < 55)
            {
                if (gameTime.TotalGameTime - lastTimeDamagedPlayer > TimeSpan.FromMilliseconds(500))
                {
                    lastTimeDamagedPlayer = gameTime.TotalGameTime;
                    FollowTarget.CurrentHealth--;
                }
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (FollowTarget != null)
                FollowAndCollisionDamage(gameTime);

            if (Shooting!=null && gameTime.TotalGameTime - lastTimeShooted > TimeSpan.FromMilliseconds(1000))
            {
                lastTimeShooted = gameTime.TotalGameTime;
                ShootMonster(sprites);
            }

            if (CurrentHealth <= 0)
            {
                IsRemoved = true;
                if (random.Next(0, 100) >= 80)
                    sprites.Add(new HealthPotion(HealthPotion.healthPotionTexture, Position));
            }
            base.Update(gameTime, sprites);
        }

        public static Monster CreateSkeleton()
        {
            return new Monster(skeletonTexture)
            {
                Position = SetRandomMonsterPosition(random),
                Speed = 2.3f,
                FollowTarget = GameAction.player,
                DistanceToApproximate = 0,
                CurrentHealth = 5,
            };
        }

        public static Monster CreateSlug()
        {
            var slug = new Monster(slugTexture)
            {
                FireBall = new FireBall(monsterFireballTexture),
                Position = SetRandomMonsterPosition(random),
                Speed = 2.2f,
                FollowTarget = GameAction.player,
                DistanceToApproximate = 0,
                CurrentHealth = 4,
            };
            slug.Shooting = slug.MakeDefaultDirectionToShot;
            return slug;
        }

        public static Monster CreateWizard()
        {
            var wizard = new Monster(wizardTexture)
            {
                FireBall = new FireBall(monsterFireballTexture),
                Position = SetRandomMonsterPosition(random),
                Speed = 1.7f,
                FollowTarget = GameAction.player,
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
                var position = new Vector2(random.Next(0, GameAction.Width), random.Next(0, GameAction.Height));
                if (Vector2.Distance(position, GameAction.player.Position) > 200)
                    return position;
            }
        }
    }
}
