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
        public static int ScreenHeight;
        public static int ScreenWidth;
        public static State State = State.Menu;
        private static TimeSpan lastTimeChangingState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

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
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameAction.BackgroundFieldTexture = Content.Load<Texture2D>("background_1");
            Monster.slugTexture = Content.Load<Texture2D>("monster_zhele");
            Monster.skeletonTexture = Content.Load<Texture2D>("skelet");
            Monster.wizardTexture = Content.Load<Texture2D>("wizard");
            Monster.monsterFireballTexture = Content.Load<Texture2D>("monsterFireball");
            FireBall.fireballTexture = Content.Load<Texture2D>("fireBall");
            HealthPotion.healthPotionTexture = Content.Load<Texture2D>("healthPotion");
            GameAction.testPic = Content.Load<Texture2D>("testPic");
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

            switch (State)
            {
                case State.Menu:
                    Menu.Update();
                    if (key.IsKeyDown(Keys.Enter)) State = ChangeState(State, gameTime);
                    break;
                case State.Action:
                    GameAction.Update(gameTime);
                    if (key.IsKeyDown(Keys.Enter)) State = ChangeState(State, gameTime);
                    break;
                case State.ResultAfterGame:
                    ResultAfterGame.Update();
                    if(key.IsKeyDown(Keys.Enter)) 
                    {
                        LoadContent();
                        State = ChangeState(State, gameTime);
                    }
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
                case State.ResultAfterGame:
                    state = State.Action;
                    return state;
            }
            return state;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (State)
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