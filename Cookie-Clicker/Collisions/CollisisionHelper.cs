using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CollisionExample.Collisions
{
    public static class CollisisionHelper
    {
        /// <summary>
        /// detects collision between two BoundingCircle
        /// </summary>
        /// <param name="a">first bounding circle</param>
        /// <param name="b">second bounding circle</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius, 2) >= Math.Pow(a.Center.X - b.Center.X, 2) + Math.Pow(a.Center.Y - b.Center.Y, 2);
        }
        /// <summary>
        /// detects collision between two BoundingRectangle
        /// </summary>
        /// <param name="a">first bounding rectangle</param>
        /// <param name="b">second bounding rectangle</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right
                || a.Top > b.Bottom || a.Bottom < b.Top);
        }

        /// <summary>
        /// detects collision between a rectangle and a circle
        /// </summary>
        /// <param name="a">the boundingcirlce</param>
        /// <param name="b">the bounding rectangle</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingCircle c, BoundingRectangle r)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);
            return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nearestX, 2) + Math.Pow(c.Center.Y - nearestY, 2);
        }
        /// <summary>
        /// detects collision between a rectangle and a circle
        /// </summary>
        /// <param name="a">the boundingcirlce</param>
        /// <param name="b">the bounding rectangle</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle r, BoundingCircle c)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);
            return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nearestX, 2) + Math.Pow(c.Center.Y - nearestY, 2);
        }

        /// <summary>
        /// detects collision between a rectangle and a Vector 2
        /// </summary>
        /// <param name="a">the bounding cirlce</param>
        /// <param name="b">the vector 2</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingCircle c, Vector2 v)
        {
            float distanceSquared = Vector2.DistanceSquared(c.Center, v);
            return distanceSquared <= c.Radius * c.Radius;
        }
        /// <summary>
        /// Detects collision between a BoundingRectangle and a Vector2 (point).
        /// </summary>
        /// <param name="r">The bounding rectangle.</param>
        /// <param name="v">The Vector2 point.</param>
        /// <returns>True if the point is within the rectangle, false otherwise.</returns>
        public static bool Collides(BoundingRectangle r, Vector2 v)
        {
            return v.X >= r.Left && v.X <= r.Right && v.Y >= r.Top && v.Y <= r.Bottom;
        }
    }
}
