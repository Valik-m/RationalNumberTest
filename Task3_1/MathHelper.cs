using System;

namespace Task3_1
{
    public static class MathHelper
    {
        public static long GetGsd(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else b %= a;
            }
            return a + b;
        }

        public static long GetLcm(long a, long b)
        {
            var gsd = GetGsd(a, b);
            return Math.Abs(a*b/gsd);
        }
    }
}
