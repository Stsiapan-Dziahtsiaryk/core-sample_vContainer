using System.Collections.Generic;
using System.Linq;

namespace Domain.Extensions
{
    public static class JsonExtension
    {
        public static string GetJson(this IDictionary<string, object> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(pair => pair.Value != null ? 
                $"\"{pair.Key}\":{(pair.Value is string ? $"\"{pair.Value}\"" : pair.Value.ToString().ToLower())}" 
                : $"\"{pair.Key}\":null")) + "}";
        }
    }
}