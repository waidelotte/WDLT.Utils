using System;

namespace WDLT.Utils.Extensions
{
    public static class DateExtensions
    {
        public static DateTime Trim(this DateTime date, long ticks)
        {
            return new DateTime(date.Ticks - date.Ticks % ticks, date.Kind);
        }
    }
}