namespace Asteroids.Scripts.Core.GameMath
{
    public static class LMath
    {
        public static bool NearlyEqual(this float a, float b)
        {
            return System.Math.Abs(a - b) < float.Epsilon;
        }
    }
}