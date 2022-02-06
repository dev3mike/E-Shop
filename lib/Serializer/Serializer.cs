using Newtonsoft.Json;
using System;

namespace EShop.Libraries.Serializer
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
