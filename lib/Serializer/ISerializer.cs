using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Libraries.Serializer
{
    public interface ISerializer
    {
        string SerializeObject(object o);
        T DeserializeObject<T>(string json);
    }
}
