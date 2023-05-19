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
        State state { get; set; } = State.Menu;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameAction.BackgroundField = Content.Load<Texture2D>("background_1");
            GameAction.monsterSprite = Content.Load<Texture2D>("monster_zhele");
            Menu.Background = Content.Load<Texture2D>("Menu");
            Menu.Font = Content.Load<SpriteFont>("Font");

            var animations = new Dictionary<string, Animation>()
            {
                {"walkUp", new Animation(Content.Load<Texture2D>("walkUp"), 5) },
                {"walkLeft", new Animation(Content.Load<Texture2D>("walkLeft"), 6) },
                {"walkRight", new Animation(Content.Load<Texture2D>("walkRight"), 6) },
                {"walkDown", new Animation(Content.Load<Texture2D>("walkDown"), 6) }
            };

            GameAction.Initialise(spriteBatch, animations, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            

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
                    GameAction.Update(gameTime);
                    
                    if (key.IsKeyDown(Keys.Space)) state = ChangeState(state);
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
                    GameAction.Draw(spriteBatch);             
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    enum State
    {
        Action, 
        Menu
    }


}