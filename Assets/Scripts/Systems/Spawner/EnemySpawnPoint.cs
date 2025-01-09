using frameworks.services.events;
using frameworks.services;
using towerdefence.events;
using frameworks.ioc;
using UnityEngine;
using towerdefence.services;
using towerdefence.configs;

namespace towerdefence.systems.spawner
{
    public class EnemySpawnPoint : BaseBehaviour
    {
        [SerializeField] private Transform _Destination;

        [InjectService] private EventHandlerService mEventHandlerService;
        [InjectService] private LevelLoaderService mLevelLoaderService;

        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<StartGameEvent>(OnStartGameEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<StartGameEvent>(OnStartGameEvent);
        }

        private void OnStartGameEvent(StartGameEvent e)
        {
            LevelInfo levelInfo = mLevelLoaderService.GetLevelInfo();
            mEventHandlerService.TriggerEvent(new EnemySpawnerPointRegisterEvent(this,
                _Destination.position, levelInfo.MaxEnemies, levelInfo.SpawnInterval, levelInfo.EnemySpeed));
        }
    }
}
