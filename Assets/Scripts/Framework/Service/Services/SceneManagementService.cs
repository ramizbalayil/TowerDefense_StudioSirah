using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace frameworks.services.scenemanagement
{
    public class SceneManagementService : BaseService
    {
        public AsyncOperation LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation toLoadScene = SceneManager.LoadSceneAsync(sceneIndex);

            return toLoadScene;
        }
    }
}

