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
        public static int currentHealth { get; set; }
        private static Texture2D container, lifeBar;
        private static Vector2 position = new Vector2(10, 10);
        private static int fullHealth;
        private static Color barColor;

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
                currentHealth, lifeBar.Height), barColor);
            spriteBatch.Draw(container, position, Color.White);
        }

        private void UpdateHealthColor()
        {
            if (currentHealth > lifeBar.Width * 0.80)
                barColor = Color.Green;
            else if (currentHealth > lifeBar.Width * 0.50)
                barColor = Color.Yellow;
            else if (currentHealth > lifeBar.Width * 0.20)
                barColor = Color.Orange;
            else
                barColor = Color.Red;
        }
    }
}
