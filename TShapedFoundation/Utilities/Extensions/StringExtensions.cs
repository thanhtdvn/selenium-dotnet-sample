using System;
using System.Linq;

namespace TShapedFoundation.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool IgnoreOrderEquals(this string[] s, string[] d)
        {
            return (s == null && d == null)
                || (s != null && d != null
                    && s.Length == d.Length
                    && s.OrderBy(x => x).SequenceEqual(d.OrderBy(x => x)));
        }
    }
}
