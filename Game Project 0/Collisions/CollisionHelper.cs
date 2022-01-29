using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game_Project_0.Collisions
{
    public class CollisionHelper
    {

        /// <summary>
        /// Detects collision between two bounding circles
        /// </summary>
        /// <param name="a">The first bounding circle</param>
        /// <param name="b">The second bounding circle</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius, 2) >=
                Math.Pow(a.Center.X - b.Center.X, 2) +
                Math.Pow(a.Center.Y - b.Center.Y, 2);
        }

    }
}
