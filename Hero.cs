using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProj
{
    class Hero : Sprite
    {

        public Hero(Texture2D texture) : base(texture)
        {
        }

        public override void Update()
        {
            Move();
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Position.X -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Position.X += Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                Position.Y -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                Position.Y += Speed;
            }

            Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Game1.ScreenWidth - this.Rectangle.Width, Game1.ScreenHeight - this.Rectangle.Height));
        }
    }
}
