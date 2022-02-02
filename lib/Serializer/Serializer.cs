using Newtonsoft.Json;
using System;

namespace Eshop.Libraries.Serializer
{
    public class Serializer : ISerializer
    {
        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string SerializeObject(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
