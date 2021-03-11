using System;
using System.Collections.Generic;
using System.Linq;

namespace WDLT.Utils.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(this IEnumerable<TValue> values, int chunkSize)
        {
            if (chunkSize > 0)
            {
                return values
                    .Select((v, i) => new { v, groupIndex = i / chunkSize })
                    .GroupBy(x => x.groupIndex)
                    .Select(g => g.Select(x => x.v));
            }

            return new[] { values };
        }

        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> collection, int[] sort)
        {
            return sort.Length == collection.Count() ? sort.Select(collection.ElementAt) : collection;
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
            => self.Select((item, index) => (item, index));

        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}