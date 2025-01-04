using frameworks.services;
using frameworks.services.scenemanagement;
using System.Collections;
using UnityEngine;

namespace frameworks.utils
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private int bootSceneIndex = 1;

        private void Start()
        {
            StartCoroutine(BootApplication());
        }

        private IEnumerator BootApplication()
        {
            yield return null;

            yield return new WaitUntil(() => ServiceBinder.IsInitialized);

            ServiceRegistry.Get<SceneManagementService>().LoadSceneAsync(bootSceneIndex);
        }
    }
}