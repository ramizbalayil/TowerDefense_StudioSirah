using UnityEngine;

namespace frameworks.services
{
    public class ServiceBinder : MonoBehaviour
    {
        #region Public Members
        public static bool IsInitialized { get; protected set; }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        protected virtual void Start()
        {
            ServiceRegistry.Clear();
        }

        private void OnDestroy()
        {
            ServiceRegistry.Clear();
        }
        #endregion
    }
}