using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace AutoRobo.Core
{
    public class ServiceLocator : IServiceLocator
    {
        // A MAP BETWEEN CONTRACTS -> CONCRETE IMPLEMENTATION CLASSES
        private IDictionary<Type, Type> servicesType;

        // A MAP CONTAINING REFERENCES TO CONCRETE IMPLEMENTATION ALREADY INSTANTIATED
        // (THE SERVICE LOCATOR USES LAZY INSTANTIATION).
        private IDictionary<Type, object> instantiatedServices;
        private static readonly object TheLock = new Object();

        private static IServiceLocator instance;
        public static IServiceLocator Instance
        {
            get
            {
                lock (TheLock) // THREAD SAFETY
                {
                    if (instance == null)
                    {
                        instance = new ServiceLocator();
                    }
                }

                return instance;
            }
        }

        private  ServiceLocator()
        {
            this.servicesType = new Dictionary<Type, Type>();
            this.instantiatedServices = new Dictionary<Type, object>();

            this.BuildServiceTypesMap();
        }

     
        public T GetService<T>()
        {
            if (this.instantiatedServices.ContainsKey(typeof(T)))
            {
                return (T)this.instantiatedServices[typeof(T)];
            }
            else
            {
                // LAZY INITIALIZATION
                try
                {
                    // USE REFLECTION TO INVOKE THE SERVICE
                    ConstructorInfo constructor = servicesType[typeof(T)].GetConstructor(new Type[0]);
                    Debug.Assert(constructor != null, "Cannot find a suitable constructor for " + typeof(T));

                    T service = (T)constructor.Invoke(null);

                    // ADD THE SERVICE TO THE ONES THAT WE HAVE ALREADY INSTANTIATED
                    instantiatedServices.Add(typeof(T), service);

                    return service;
                }
                catch (KeyNotFoundException)
                {
                    throw new ApplicationException("The requested service is not registered");
                }
            }
        }

        private void BuildServiceTypesMap()
        {
        }
        /// <summary>
        /// 注册类型，获取未发现，反射构造实例
        /// </summary>
        /// <param name="IServiceType"></param>
        /// <param name="ServiceType"></param>
        public void RegisterType(Type IServiceType, Type ServiceType) {
            if (!servicesType.Keys.Contains(IServiceType)) {
                servicesType.Add(IServiceType, ServiceType);
            }
        }

        /// <summary>
        /// 注册实例类型
        /// </summary>
        /// <param name="IServiceType"></param>
        /// <param name="ServiceType"></param>
        public void RegisterInstanceType(Type IServiceType, object serviceInstance)
        {
            if (!servicesType.Keys.Contains(IServiceType))
            {
                servicesType.Add(IServiceType, serviceInstance.GetType());
                instantiatedServices.Add(IServiceType, serviceInstance);
            }         
        }
    }
}
