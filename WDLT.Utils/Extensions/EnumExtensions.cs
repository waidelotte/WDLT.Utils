using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace WDLT.Utils.Extensions
{
    public static class EnumExtensions
    {
        private static readonly Random _random = new Random();

        public static string DisplayAttribute(this Enum e)
        {
            return e.GetAttributeOfType<DisplayAttribute>()?.Name ?? e.ToString();
        }

        public static string EnumMemberAttribute(this Enum e)
        {
            return e.GetAttributeOfType<EnumMemberAttribute>()?.Value ?? e.ToString();
        }

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            if (enumVal == null) return default;

            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());

            try
            {
                var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
                return attributes.Length > 0 ? (T)attributes[0] : null;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        public static IEnumerable<T> ToFlagsList<T>(this T FromSingleEnum) where T : struct
        {
            return FromSingleEnum.ToString()
                ?.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(
                    strenum =>
                    {
                        Enum.TryParse(strenum, true, out T outenum);
                        return outenum;
                    });
        }

        public static T[] GetEnumValues<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("GetValues<T> can only be called for types derived from System.Enum", "T");
            }
            return (T[])Enum.GetValues(typeof(T));
        }

        public static T RandomEnum<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(_random.Next(v.Length));
        }
    }
}