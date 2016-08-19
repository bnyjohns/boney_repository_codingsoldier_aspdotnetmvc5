using System;
using System.Collections.Generic;

namespace CodingSoldier.Core.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<To> ConvertTo<To, From>(this IEnumerable<From> input)
                                                                    where To : class where From : class
        {
            foreach (var inputItem in input)
            {
                var outputItem = Activator.CreateInstance(typeof(To), inputItem) as To;
                yield return outputItem;
            }
        }
    }
}
