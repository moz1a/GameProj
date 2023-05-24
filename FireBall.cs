using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProj
{
    public class FireBall : Sprite
    {
        private float timer;
        public FireBall(Texture2D texture) 
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(timer > LifeSpan)
                IsRemoved = true;
            Position += Direction * LinearVelocity * 4;
        }
    }
}
