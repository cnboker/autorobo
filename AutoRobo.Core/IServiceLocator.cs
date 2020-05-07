using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void RegisterType(Type IServiceType, Type ServiceType);
        void RegisterInstanceType(Type IServiceType, object serviceInstance);
    }
}
