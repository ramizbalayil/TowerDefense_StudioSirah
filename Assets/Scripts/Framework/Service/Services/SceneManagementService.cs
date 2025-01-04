using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace frameworks.services.scenemanagement
{
    public class SceneManagementService : BaseService
    {
        public async override Task Init()
        {
            await base.Init();
            LoadSceneAsync(2);
        }

        public AsyncOperation LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation toLoadScene = SceneManager.LoadSceneAsync(sceneIndex);

            return toLoadScene;
        }
    }
}

