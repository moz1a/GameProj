
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProj
{
    class Sprite
    {
        private Texture2D texture;
        public Vector2 Position;
        public float Speed = 5f;
        public Input Input;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Update()
        {
            Move();
        }

        private void Move()
        {
            if (Input==null) return;

            if(Keyboard.GetState().IsKeyDown(Input.Left))
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
