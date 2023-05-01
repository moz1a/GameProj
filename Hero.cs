using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProj
{
    class Hero : Sprite
    {

        public Hero(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            foreach (var sprite in sprites)
            {
                if (sprite == this) continue;

                if((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 && this.IsTouchingRight(sprite)))
                    this.Velocity.X = 0;

                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)))
                    this.Velocity.Y = 0;
            }
            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;

            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;

            Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Game1.ScreenWidth - this.Rectangle.Width, Game1.ScreenHeight - this.Rectangle.Height));
        }
    }
}
