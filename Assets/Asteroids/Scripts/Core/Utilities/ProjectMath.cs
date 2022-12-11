using UnityEngine;

namespace Asteroids.Scripts.Core.Utilities
{
    public static class ProjectMath
    {
        public static bool NearlyEqual(this float a, float b)
        {
            return System.Math.Abs(a - b) < float.Epsilon;
        }
        
        public static Vector2 RandomPointOutsideRectangle(float width, float height)
        {
            var point = Random.Range(0, width + width + height + height);
            Vector2 result = new Vector2();
            if (point < (width + height))
            {
                if (point < width)
                {
                    result.x = point;
                    result.y = 0;
                }
                else
                {
                    result.x = width;
                    result.y = point - width;
                }
            }
            else
            {
                point -= (width + height);
                if (point < width)
                {
                    result.x = width - point;
                    result.y = height;
                }
                else
                {
                    result.x = 0;
                    result.y = height - (point - width);
                }
            }

            return result;
        }
    }
}