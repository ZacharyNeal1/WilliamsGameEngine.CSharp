using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Conv
    {
        public static Vector2 ToVect2(Vector2f e)
        {
            return new Vector2(e.X ,e.Y);

        }
        public static Vector2f ToVect2f(Vector2 e)
        {
            return new Vector2f(e.X, e.Y);

        }
    }
}
