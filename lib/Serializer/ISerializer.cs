using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Libraries.Serializer
{
    public interface ISerializer
    {
        string SerializeObject(object o);
        T DeserializeObject<T>(string json);
    }
}
