using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtension
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }
}