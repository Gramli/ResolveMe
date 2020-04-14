using System.Collections.Generic;
using System.Linq;

namespace ResolveMe.MathCompiler.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(
            this IEnumerable<T> source, int batchCount, int batchLength)
        {
            using (var enumerator = source.GetEnumerator())
            {
                for (var i = 0; i < batchCount - 1; i++)
                {
                    if (enumerator.MoveNext())
                    {
                        yield return InnerSplit(enumerator, batchLength - 1);
                    }
                }

                if (enumerator.MoveNext())
                {
                    yield return InnerSplit(enumerator);
                }
            }
        }

        public static IList<IEnumerable<T>> SplitToArray<T>(
            this IEnumerable<T> source, int batchCount, int batchLength)
        {
            var result = new List<IEnumerable<T>>();
            using (var enumerator = source.GetEnumerator())
            {
                for (var i = 0; i < batchCount - 1; i++)
                {
                    result.Add(InnerSplitToArray(enumerator, batchLength));
                }

                result.Add(InnerSplitToArray(enumerator));
            }

            return result;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == 0; //use count because of performance
        }

        private static IEnumerable<T> InnerSplitToArray<T>(
            IEnumerator<T> source, int length = 0)
        {
            for (var i = 0; (i < length || length == 0) && source.MoveNext(); i++)
            {
                yield return source.Current;
            }
        }

        private static IEnumerable<T> InnerSplit<T>(
            IEnumerator<T> source, int length = 0)
        {
            yield return source.Current;
            for (var i = 0; (i < length || length == 0) && source.MoveNext(); i++)
            {
                yield return source.Current;
            }
        }
    }
}
