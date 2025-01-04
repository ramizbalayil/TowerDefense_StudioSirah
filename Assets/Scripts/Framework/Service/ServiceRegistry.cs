using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace frameworks.services
{
    #region Abstract class
    public abstract class BaseService
    {
        public virtual async Task Init()
        {
            await Task.Yield();
        }

        public virtual void OnDestroy() { }
    }
    #endregion

    public static class ServiceRegistry
    {
        #region Private Variables and Constructor
        private static Dictionary<Type, BaseService> serviceMap;
        public static Dictionary<Type, BaseService> ServiceMap { get { return serviceMap; } }
        static ServiceRegistry()
        {
            serviceMap = new Dictionary<Type, BaseService>();
        }
        #endregion

        #region Public Methods
        public static async Task Bind<T>(BaseService service)
        {
            try
            {
                serviceMap.Add(service.GetType(), service);
                await service.Init();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void UnBind<T>()
        {
            Type unbindServiceType = typeof(T);
            serviceMap[unbindServiceType].OnDestroy();
            serviceMap.Remove(unbindServiceType);
        }

        public static Z Get<Z>() where Z : BaseService
        {
            if (serviceMap.TryGetValue(typeof(Z), out BaseService result))
            {
                return result as Z;
            }
            return null;
        }

        public static void Clear()
        {
            foreach (BaseService service in serviceMap.Values)
            {
                service.OnDestroy();
            }
            serviceMap.Clear();
        }
        #endregion
    }
}

