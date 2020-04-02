using System.Collections.Generic;

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
