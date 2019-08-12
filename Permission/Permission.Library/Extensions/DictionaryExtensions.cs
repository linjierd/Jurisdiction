using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Library.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> d, IDictionary<TKey, TValue> adddic)
        {

            if (adddic != null && adddic.Count > 0)
            {
                if (d == null) d = new Dictionary<TKey, TValue>();
                foreach (var value in adddic)
                {
                    if (!d.Keys.Contains(value.Key))
                    {
                        d.Add(value.Key, value.Value);
                    }
                }
            }
        }
    }
}
