using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using System.Threading;


namespace GameProj
{
    public class HealthBar 
    {
        public Hero player;
        public int currentHealth
        {
            get
            {
                return player.CurrentHealth;
            }
            set { }
        }
        private Texture2D container, lifeBar;
        private Vector2 position = new Vector2(10, 10);
        private int fullHealth;
        private Color barColor;

        private int HPDivisions
        {
            get 
            {
                return fullHealth / player.maxHP;
            }
        }

        public HealthBar(ContentManager content)
        {
            container = content.Load<Texture2D>("container");
            lifeBar = content.Load<Texture2D>("healthBar");
            fullHealth = lifeBar.Width;
            currentHealth = fullHealth;
        }

        public void Update()
        {
            UpdateHealthColor();
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(lifeBar, position, new Rectangle((int)position.X, (int)position.Y,
                GetHP(), lifeBar.Height), barColor);
            spriteBatch.Draw(container, position, Color.White);
        }

        private void UpdateHealthColor()
        {
            if (GetHP() > lifeBar.Width * 0.80)
                barColor = Color.Green;
            else if (GetHP() > lifeBar.Width * 0.50)
                barColor = Color.Yellow;
            else if (GetHP() > lifeBar.Width * 0.20)
                barColor = Color.Orange;
            else
                barColor = Color.Red;
        }

        private int GetHP()
        {
            return HPDivisions * currentHealth;
        }
    }
}
