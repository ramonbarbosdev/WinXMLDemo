using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinXMLDemo
{
    public static class DictionaryExtensao
    {

        public static Dictionary<TKey, TValue> Reverse2<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
        }

    }
}
