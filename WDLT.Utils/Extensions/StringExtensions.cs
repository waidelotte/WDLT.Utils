using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace WDLT.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string ToAlphaNumeric(this string input)
        {
            return new Regex("[^a-zA-Z0-9]").Replace(input, "");
        }

        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };

        public static double ParseCurrency(this string input)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .GroupBy(c => c.NumberFormat.CurrencySymbol)
                .ToDictionary(c => c.Key, c => c.First());

            var culture = cultures.FirstOrDefault(c => input.Contains(c.Key));

            double result;
            if (!culture.Equals(default(KeyValuePair<string, CultureInfo>)))
            {
                input = input.Replace(culture.Key, "");
                result = double.Parse(input, CultureInfo.InvariantCulture);
            }
            else
            {
                if (!double.TryParse(input, NumberStyles.Currency, CultureInfo.InvariantCulture, out result))
                {
                    throw new Exception("Invalid number format");
                }
            }

            return result;
        }

        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
            return $"{input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim()}…";
        }
    }
}
