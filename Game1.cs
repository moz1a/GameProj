using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading;

namespace GameProj
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public static State state = State.Menu;
        private static TimeSpan lastTimeChangingState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            
        }
        GameWindow window;
        bool isBorderless = false;
        private void SetFullScreen()
        {
            ScreenWidth = Window.ClientBounds.Width;
            ScreenHeight = Window.ClientBounds.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.HardwareModeSwitch = !isBorderless;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            SetFullScreen();

            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameAction.BackgroundField = Content.Load<Texture2D>("background_1");
            GameAction.monsterSprite = Content.Load<Texture2D>("Monster");
            GameAction.fireballSprite = Content.Load<Texture2D>("fireBall");
            ResultAfterGame.Font = Content.Load<SpriteFont>("FontForResult");
            ResultAfterGame.Background = Content.Load<Texture2D>("gameOver");
            Menu.Background = Content.Load<Texture2D>("Menu");
            Menu.Font = Content.Load<SpriteFont>("Font");
            GameAction.healthBar = new HealthBar(Content);

            var animations = new Dictionary<string, Animation>()
            {
                {"walkUp", new Animation(Content.Load<Texture2D>("walkUp"), 6) },
                {"walkLeft", new Animation(Content.Load<Texture2D>("walkLeft"), 6) },
                {"walkRight", new Animation(Content.Load<Texture2D>("walkRight"), 6) },
                {"walkDown", new Animation(Content.Load<Texture2D>("walkDown"), 6) }
            };

            GameAction.Initialise(spriteBatch, animations, ScreenWidth, ScreenHeight);
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
                    if (key.IsKeyDown(Keys.Enter)) state = ChangeState(state, gameTime);
                    break;
                case State.Action:
                    GameAction.Update(gameTime);
                    if (key.IsKeyDown(Keys.Enter)) state = ChangeState(state, gameTime);
                    break;
                case State.ResultAfterGame:
                    ResultAfterGame.Update();
                    break;
            }

            base.Update(gameTime);
        }

        private static State ChangeState(State state, GameTime gameTime)
        {
            if (gameTime.TotalGameTime - lastTimeChangingState < TimeSpan.FromSeconds(1))
                return state;

            lastTimeChangingState = gameTime.TotalGameTime;
          
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
                    GameAction.Draw(spriteBatch);             
                    break;
                case State.ResultAfterGame:
                    ResultAfterGame.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public enum State
    {
        Action, 
        Menu,
        ResultAfterGame
    }
}