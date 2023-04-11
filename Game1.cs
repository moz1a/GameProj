using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace GameProj
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        //public List<Creature> Creatures = new List<Creature>();

        public Texture2D hero;
        public Texture2D backgroundField;
        Vector2 frogPosition;
        float ballSpeed;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            frogPosition = new Vector2(graphics.PreferredBackBufferWidth / 2,
                                       graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 200f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundField = Content.Load<Texture2D>("background");
            hero = Content.Load<Texture2D>("crocofrog");
            Menu.Background = Content.Load<Texture2D>("Menu");
            Menu.Font = Content.Load<SpriteFont>("Font");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                frogPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                frogPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                frogPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                frogPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            Menu.Update();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //_spriteBatch.Draw(backgroundField,new Rectangle(0, 0, 1920, 1080), Color.White);
            Menu.Draw(spriteBatch);
            //spriteBatch.Draw(hero, frogPosition, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public Creature MakeCreature(Creatures creature)
        {
            var result = new Creature();

            if (creature == GameProj.Creatures.crocoFrog)
            {
                return result;
            }
            return null;

        }
    }

    public class Texture
    {
        public Texture2D texture;
    }

    public class Position
    {
        Vector2 position;
    }

    public class Creature
    {
         public Texture Texture;
         public LifeCharacteristics LifeParams;
         public Position Position;

    }

    public class LifeCharacteristics
    {
        public int HP;
        public double Speed;
        public double Strength;
        public double Range;
        public double FireRate;
        public double BallSpeed;
    }

    public enum Creatures
    {
        crocoFrog
    }
}