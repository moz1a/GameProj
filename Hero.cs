using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProj
{
    class Hero : Sprite
    {
        public Attributes StandartAttributes { get; set; }

        public List<Attributes> AttributesModifiers { get; set; }

        public Attributes TotalAttributes
        {
            get
            {
                return StandartAttributes + AttributesModifiers.Sum();
            }
        }

        public Hero(Dictionary<string, Animation> animations) 
            : base(animations)
        {
            StandartAttributes = new Attributes();
            AttributesModifiers = new List<Attributes>(); 
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Speed = TotalAttributes.Speed;
            
            base.Update(gameTime, sprites);
        }

        //public void GetHit(List<Sprite> sprites)
        //{
        //    if (CheckCollision(sprites))
        //        HealthBar.currentHealth -= 25;
        //}
    }
}
