using System.Collections.Generic;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class CustomDictionary<K, V> : Dictionary<K, V>
    {
        public void AddRange(Dictionary<K, V> dic)
        {
            foreach (KeyValuePair<K, V> entry in dic)
                this.Add(entry.Key, entry.Value);
        }
    }
}