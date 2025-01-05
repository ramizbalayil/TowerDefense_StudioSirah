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
        [InjectService] private EventHandlerService mEventHandlerService;

        protected override void Awake()
        {
            base.Awake();
            InitialiseLevel();
            mEventHandlerService.AddListener<LevelCompletedEvent>(OnLevelCompletedEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<LevelCompletedEvent>(OnLevelCompletedEvent);
        }

        private void OnLevelCompletedEvent(LevelCompletedEvent e)
        {
            if (e.Won)
                mLevelLoaderService.UnlockNextLevel();
        }

        private void InitialiseLevel()
        {
            LevelInfo levelInfo = mLevelLoaderService.GetLevelInfo();
            Instantiate(levelInfo.LevelPrefab);
        }
    }
}
