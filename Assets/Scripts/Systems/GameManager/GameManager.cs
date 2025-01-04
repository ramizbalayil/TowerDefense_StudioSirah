using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using UnityEngine;
using towerdefence.events;
using towerdefence.services;

namespace towerdefence.systems.manager
{
    public class GameManager : BaseBehaviour
    {
        [InjectService] private EventHandlerService mEventHandlerService;
        [InjectService] private ScoreKeeperService mScoreKeeperService;

        private int mMaxEnemies;
        private int mEnemiesAlive;

        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<EnemySpawnerPointRegisterEvent>(OnEnemySpawnerPointRegisterEvent);
            mEventHandlerService.AddListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<EnemySpawnerPointRegisterEvent>(OnEnemySpawnerPointRegisterEvent);
            mEventHandlerService.RemoveListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
        }

        private void OnEnemyReachedDestinationEvent(EnemyReachedDestinationEvent e)
        {
            mEnemiesAlive -= 1;
            if (mEnemiesAlive == 0)
            {
                mEventHandlerService.TriggerEvent(new LevelCompletedEvent(mScoreKeeperService.HasScoreLeft()));
            }
        }

        private void OnEnemySpawnerPointRegisterEvent(EnemySpawnerPointRegisterEvent e)
        {
            mMaxEnemies = e.MaxEnemies;
            mEnemiesAlive = mMaxEnemies;
        }
    }
}