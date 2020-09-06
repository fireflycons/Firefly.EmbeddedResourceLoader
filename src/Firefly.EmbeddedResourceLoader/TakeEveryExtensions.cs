namespace Firefly.EmbeddedResourceLoader
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Linq extension to take every nth element of a sequence.
    /// </summary>
    /// <seealso href="http://www.blackwasp.co.uk/TakeEvery.aspx"/>
    public static class TakeEveryExtensions
    {
        /// <summary>
        /// Takes every nth element from a sequence.
        /// </summary>
        /// <typeparam name="T">Type of sequence</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="every">Number of elements to advance before taking another element.</param>
        /// <returns>New enumerable containing selected elements.</returns>
        public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> sequence, int every)
        {
            return sequence.TakeEvery(every, 0);
        }

        /// <summary>
        /// Takes every nth element from a sequence.
        /// </summary>
        /// <typeparam name="T">Type of sequence</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="every">Number of elements to advance before taking another element.</param>
        /// <param name="skipInitial">Number of elements to skip before beginning to take elements.</param>
        /// <returns>New enumerable containing selected elements.</returns>
        /// <exception cref="ArgumentNullException">sequence is <c>null</c></exception>
        /// <exception cref="ArgumentException">
        /// 'every' must be 1 or greater
        /// or
        /// 'skipInitial' must be 0 or greater
        /// </exception>
        public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> sequence, int every, int skipInitial)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            if (every < 1)
            {
                throw new ArgumentException("'every' must be 1 or greater");
            }

            if (skipInitial < 0)
            {
                throw new ArgumentException("'skipInitial' must be 0 or greater");
            }

            return TakeEveryImpl(sequence, every, skipInitial);
        }

        /// <summary>
        /// Implementation helper
        /// </summary>
        /// <typeparam name="T">Type of sequence</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="every">Number of elements to advance before taking another element.</param>
        /// <param name="toSkip">Number of elements to skip before beginning to take elements.</param>
        /// <returns>Yields selected elements to enumerable.</returns>
        private static IEnumerable<T> TakeEveryImpl<T>(IEnumerable<T> sequence, int every, int toSkip)
        {
            var enumerator = sequence.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (toSkip == 0)
                {
                    yield return enumerator.Current;
                    toSkip = every;
                }

                toSkip--;
            }
        }
    }
}