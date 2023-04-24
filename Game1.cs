using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading;

namespace GameProj
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static int ScreenHeight;
        public static int ScreenWidth;
        //public List<Creature> Creatures = new List<Creature>();
        State state { get; set; } = State.Menu;

        private Sprite monster;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameAction.BackgroundField = Content.Load<Texture2D>("background_1");
            Hero.texture = Content.Load<Texture2D>("knight");
            Menu.Background = Content.Load<Texture2D>("Menu");
            Menu.Font = Content.Load<SpriteFont>("Font");
            GameAction.Initialise(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            var monsterTexture = Content.Load<Texture2D>("Monster");
            monster = new Sprite(monsterTexture) { Input = new Input { Down = Keys.S, Left = Keys.A, Right = Keys.D, Up = Keys.W } };
            monster.Position = new Vector2(100, 100);
            
        }

        protected override void Update(GameTime gameTime)
        {
            var key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Escape))
                Exit();

            switch (state)
            {
                case State.Menu:
                    Menu.Update();
                    if (key.IsKeyDown(Keys.Space)) state = ChangeState(state);
                    break;
                case State.Action:
                    GameAction.Update();
                    if (key.IsKeyDown(Keys.W)) GameAction.Hero.Up();
                    if (key.IsKeyDown(Keys.S)) GameAction.Hero.Down();
                    if (key.IsKeyDown(Keys.D)) GameAction.Hero.Right();
                    if (key.IsKeyDown(Keys.A)) GameAction.Hero.Left();
                    if (key.IsKeyDown(Keys.Space)) state = ChangeState(state);
                    monster.Update();
                    break;
            }

            base.Update(gameTime);
        }

        private static State  ChangeState(State state)
        {
            Thread.Sleep(100);
            switch (state)
            {
                case State.Menu:
                    state = State.Action;
                    return state;
                case State.Action:
                    state = State.Menu;
                    return state;
            }
            return state;
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
                    monster.Draw(spriteBatch);
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