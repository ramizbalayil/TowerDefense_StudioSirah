using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using System;
using towerdefence.configs;
using towerdefence.events;
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
            LevelInfo levelInfo = mLevelLoaderService.GetLevelInfo();
            Instantiate(levelInfo.LevelPrefab);
        }
    }
}
