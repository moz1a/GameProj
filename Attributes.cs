
namespace GameProj
{
    public class Attributes
    {
        public float Speed { get; set; }

        public static Attributes operator +(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                Speed = a.Speed + b.Speed
            };
        }
        public static Attributes operator -(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                Speed = a.Speed - b.Speed
            };
        }
    }
}
