using frameworks.services;
using frameworks.services.scenemanagement;
using frameworks.ui;

namespace towerdefence.ui
{
    public class UILevelSelect : UIScreen
    {
        [InjectService] SceneManagementService mSceneManagementService;

        public void OnLevelSelected(int level)
        {
            mSceneManagementService.LoadSceneAsync(2);
        }
    }
}