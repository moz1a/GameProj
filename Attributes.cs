using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GameProj
{
    public class Attributes
    {
        //public int HealthPoints { get; set; }   

        public float Speed { get; set; }

        public static Attributes operator +(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                //HealthPoints = a.HealthPoints + b.HealthPoints,
                Speed = a.Speed + b.Speed
            };
        }
        public static Attributes operator -(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                //HealthPoints = a.HealthPoints - b.HealthPoints,
                Speed = a.Speed - b.Speed
            };
        }
    }
}
