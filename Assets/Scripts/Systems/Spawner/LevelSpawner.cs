using frameworks.ioc;
using frameworks.services;
using towerdefence.configs;
using towerdefence.services;

namespace towerdefence.systems.spawner
{
    public class LevelSpawner : BaseBehaviour
    {
        [InjectService] private LevelLoaderService mLevelLoaderService;

        protected override void Awake()
        {
            base.Awake();
            InitialiseLevel();
        }

        private void InitialiseLevel()
        {
            LevelInfo levelInfo = mLevelLoaderService.UnloadLevelInfo();
            Instantiate(levelInfo.LevelPrefab);
        }
    }
}
