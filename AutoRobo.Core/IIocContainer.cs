using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core
{
    public interface IIocContainer
    {
        object Get(Type type);
        T Get<T>();
        T Get<T>(string name, string value);
        void Inject(object item);
        T TryGet<T>();
    }
}
