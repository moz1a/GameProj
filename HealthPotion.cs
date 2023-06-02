﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace GameProj
{
    public class HealthPotion : Sprite
    {
        public static Texture2D healthPotionTexture { get; set; }
        public HealthPotion(Texture2D texture, Vector2 position) : base(texture)
        {
            Position = position;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (CheckRectangleCollision(sprite) && sprite is Hero)
                {
                    if ((sprite as Hero).CurrentHealth < (sprite as Hero).maxHealth)
                        (sprite as Hero).CurrentHealth++;
                    IsRemoved = true;
                }
            }
        }
    }
}
