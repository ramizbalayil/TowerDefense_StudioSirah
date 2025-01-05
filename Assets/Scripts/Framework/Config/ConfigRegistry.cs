using System;
using System.Collections.Generic;
using UnityEngine;

namespace frameworks.configs
{
    #region Base Config
    public class BaseConfig : ScriptableObject
    {

    }
    #endregion

    public static class ConfigRegistry
    {
        #region Private Variables and Constructor
        private static Dictionary<Type, BaseConfig> configMap;
        public static Dictionary<Type, BaseConfig> ConfigMap { get { return configMap; } }
        static ConfigRegistry()
        {
            configMap = new Dictionary<Type, BaseConfig>();
        }
        #endregion

        #region Public Methods
        public static void Bind(BaseConfig config)
        {
            try
            {
                configMap.Add(config.GetType(), config);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static Z Get<Z>() where Z : BaseConfig
        {
            if (configMap.TryGetValue(typeof(Z), out BaseConfig result))
            {
                return result as Z;
            }
            return null;
        }

        public static void Clear()
        {
            configMap.Clear();
        }
        #endregion
    }
}

