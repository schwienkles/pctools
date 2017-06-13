using System;
using System.Collections.Generic;


namespace PCToolsManager
{
    public static class Helper
    {
        /// <summary>
        /// Gets the difference in time between this and other, if this is 23:59 and other 00:01 (or the other way around)
        /// Then the value will for both instances be the same (00:02)
        /// </summary>
        /// <param name="span">The current TimeSpan</param>
        /// <param name="other">The TimeSpan to compare to</param>
        /// <returns></returns>
        public static TimeSpan GetSmallestDifference(this TimeSpan span, TimeSpan other)
        {
            TimeSpan a = span.Subtract(other);
            TimeSpan b = other.Subtract(span);

            return a < b ? a : b;
        }

        public static void MoveToBack<T>(this List<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index < 0)
            {
                throw new InvalidOperationException(nameof(item));
            }

            if (index == list.Count - 1)
            {
                return;
            }

            list.Remove(item);
            list.Insert(index+1, item);
        }

        public static void MoveToFront<T>(this List<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index < 0)
            {
                throw new InvalidOperationException(nameof(item));
            }

            if (index == 0)
            {
                return;
            }

            list.Remove(item);
            list.Insert(index - 1, item);
        }
    }
}
