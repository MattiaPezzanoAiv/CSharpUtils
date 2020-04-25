namespace MP.CSharpUtilities.Extensions
{
    public static class Extensions
    {
        public static bool AlmostEqual(this float val, float other, float tolerance = 0.005f)
        {
            return val >= other - tolerance && val <= other + tolerance;
        }

        public static bool AlmostZero(this float val, float tolerance = 0.005f)
        {
            return AlmostEqual(val, 0f, tolerance);
        }

        public static float Clamp(this float val, float min, float max)
        {
            if(val > max)
            {
                val = max;
            }
            if(val < min)
            {
                val = min;
            }

            return val;
        }
    }
}
