using System;
using System.Collections.Generic;
using System.Linq;

namespace mapmyfitnessapi_sdk.extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts an enumerable to a dictionary.  If it can't, then gives a good reason why
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="elementSelector"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TElement> ToDictionaryExplicit<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector.Equals(null)) throw new ArgumentNullException("keySelector");
            if (elementSelector.Equals(null)) throw new ArgumentNullException("keySelector");

            try
            {
                return source.ToDictionary(keySelector, elementSelector);
            }
            catch (ArgumentException exception)
            {
                var duplicates = source.GroupBy(keySelector).Where(g => g.Count() > 1).Select(g => g.Key).ToArray();
                var duplicateMessage = string.Join(",", duplicates);
                var message = string.Format("Unable to convert to dictionary since keys are not unique. Duplicate keys are: {0}", duplicateMessage);

                throw new ArgumentException(message, exception);
            }
        }

        /// <summary>
        /// Converts an enumerable to a dictionary.  If it can't, then gives a good reason why
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TSource> ToDictionaryExplicit<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector.Equals(null)) throw new ArgumentNullException("keySelector");

            try
            {
                return source.ToDictionary(keySelector);
            }
            catch (ArgumentException exception)
            {
                var duplicates = source.GroupBy(keySelector).Where(g => g.Count() > 1).Select(g => g.Key).ToArray();
                var duplicateMessage = string.Join(",", duplicates);
                var message = string.Format("Unable to convert to dictionary since keys are not unique. Duplicate keys are: {0}",duplicateMessage);

                throw new ArgumentException(message, exception);
            }
        }

        /// <summary>
        /// Tries to get a value from a dictionary.  If it can't, then tells what the keys are
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Value<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (key.Equals(null)) throw new ArgumentNullException("key");

            try
            {
                return source[key];
            }
            catch (KeyNotFoundException exception)
            {
                var keysMessage = string.Join(",", source.Keys);
                var message = string.Format("Unable to find value for key '{0}'. Available keys are: {0}",keysMessage);

                throw new KeyNotFoundException(message, exception);
            }
        }
    }
}