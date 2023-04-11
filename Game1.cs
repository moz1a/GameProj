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
        State state = State.Menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameAction.BackgroundField = Content.Load<Texture2D>("background");
            Hero.texture = Content.Load<Texture2D>("knight");
            Menu.Background = Content.Load<Texture2D>("Menu");
            Menu.Font = Content.Load<SpriteFont>("Font");
            GameAction.Initialise(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            var key = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || key.IsKeyDown(Keys.Escape))
                Exit();

            switch (state)
            {
                case State.Menu:
                    Menu.Update();
                    if (key.IsKeyDown(Keys.Space)) 
                        state = State.Action;
                    break;
                case State.Action:
                    GameAction.Update();
                    if (key.IsKeyDown(Keys.W)) GameAction.Hero.Up();
                    if (key.IsKeyDown(Keys.S)) GameAction.Hero.Down();
                    if (key.IsKeyDown(Keys.D)) GameAction.Hero.Right();
                    if (key.IsKeyDown(Keys.A)) GameAction.Hero.Left();
                    if (key.IsKeyDown(Keys.Space))
                        state = State.Menu;
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (state)
            {
                case State.Menu:
                    Menu.Draw(spriteBatch);
                    break;
                case State.Action:
                    GameAction.Draw();
                    break;

            }
            spriteBatch.End();
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

    enum State
    {
        Action, 
        Menu
    }

    public class Creature
    {
         public Texture Texture;
         public LifeCharacteristics LifeParams;

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