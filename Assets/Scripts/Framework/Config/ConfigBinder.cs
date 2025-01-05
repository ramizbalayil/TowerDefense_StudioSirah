using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace frameworks.configs
{
    public class ConfigBinder : MonoBehaviour
    {
        #region Public Members
        public static bool IsInitialized { get; private set; }
        #endregion

        #region Public Members
        [SerializeField] private List<BaseConfig> configs = new List<BaseConfig>();
        #endregion

        #region Unity Functions
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator Start()
        {
            ConfigRegistry.Clear();

            foreach (BaseConfig config in configs)
            {
                ConfigRegistry.Bind(config);
            }

            IsInitialized = true;
            yield return null;
        }

        private void OnDestroy()
        {
            ConfigRegistry.Clear();
        }
        #endregion
    }
}
