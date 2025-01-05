using frameworks.ioc;
using UnityEngine;

namespace frameworks.utils
{
    public class Singleton<T> : BaseBehaviour where T : MonoBehaviour
    {
        #region Member Variables

        private static T instance;

        protected bool initialized;

        #endregion

        #region Properties

        public static T Instance
        {
            get
            {
                // If the instance is null then either Instance was called to early or this object is not active.
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<T>();

                    (instance as Singleton<T>).Initialize();
                }

                if (instance == null)
                {
                    Debug.LogWarningFormat("[SingletonComponent] Returning null instance for component of type {0}.", typeof(T));
                }

                return instance;
            }
        }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            SetInstance();
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        #endregion

        #region Public Methods

        public static bool Exists()
        {
            return instance != null;
        }

        public bool SetInstance()
        {
            if (instance != null && instance != gameObject.GetComponent<T>())
            {
                Debug.LogWarning("[SingletonComponent] Instance already set for type " + typeof(T));
                return false;
            }

            instance = gameObject.GetComponent<T>();

            Initialize();

            return true;
        }

        #endregion

        #region Protected Methods

        protected virtual void OnInitialize()
        {

        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            if (!initialized)
            {
                OnInitialize();
                initialized = true;
            }
        }

        #endregion
    }
}

