using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPDF.Client.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<TSource>(this ICollection<TSource> list) =>
            list == null || list.Count == 0;
        public static bool IsNull(this object objeto)
            => objeto == null;
    }
}
