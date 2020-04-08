using System.Collections.Generic;
using System.Linq;

namespace SubtitleDownloader.Data
{
    public static class IEnumerable
    {
        /// <summary>
        /// Support Index In Foreach loop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        {
            return self.Select((item, index) => (item, index));
        }
    }
}
