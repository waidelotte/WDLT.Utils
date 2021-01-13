using System;

namespace WDLT.Utils.Extensions
{
    public static class NumsExtensions
    {
        public static bool IsWithin(this int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }

        public static bool IsWithin(this double value, double minimum, double maximum)
        {
            return value >= minimum && value <= maximum;
        }

        public static bool IsDivisble(this double x, double n)
        {
            return x % n == 0;
        }

        public static double Round(this double value, int digits)
        {
            return Math.Round(value, digits);
        }

        public static int Round(this int i, int nearest)
        {
            if (nearest <= 0 || nearest % 10 != 0)
                throw new ArgumentOutOfRangeException(nameof(nearest), "Must round to a positive multiple of 10");

            return (i + 5 * nearest / 10) / nearest * nearest;
        }
    }
}