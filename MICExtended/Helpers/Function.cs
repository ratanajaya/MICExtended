using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MICExtended.Helpers
{
    /// <summary>
    /// Collection of pure functions
    /// </summary>
    public static class Function
    {
        public static IOrderedEnumerable<T> OrderByAlphaNumeric<T>(this IEnumerable<T> source, Func<T, string> selector) {
            int max = source
                .SelectMany(i => Regex.Matches(selector(i), @"\d+").Cast<Match>().Select(m => (int?)m.Value.Length))
                .Max() ?? 0;

            return source.OrderBy(i => Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
        }

        public static String BytesToString(long byteCount) {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if(byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}
