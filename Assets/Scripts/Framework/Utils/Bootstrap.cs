using frameworks.configs;
using frameworks.services;
using frameworks.services.scenemanagement;
using System.Collections;
using UnityEngine;

namespace frameworks.utils
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private int _BootSceneIndex = 1;

        private void Start()
        {
            StartCoroutine(BootApplication());
        }

        private IEnumerator BootApplication()
        {
            yield return null;

            yield return new WaitUntil(() => ServiceBinder.IsInitialized);
            yield return new WaitUntil(() => ConfigBinder.IsInitialized);


            ServiceRegistry.Get<SceneManagementService>().LoadSceneAsync(_BootSceneIndex);
        }
    }
}