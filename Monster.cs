using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProj
{
    internal class Monster : Sprite
    {
        public Monster(Texture2D texture) 
            : base(texture)
        {
            Position = new Vector2(500, 100);
        }

    }



}
