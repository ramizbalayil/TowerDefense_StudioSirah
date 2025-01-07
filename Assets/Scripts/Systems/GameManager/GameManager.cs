using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
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
            mEventHandlerService.AddListener<EnemyDeadEvent>(OnEnemyDeadEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<EnemySpawnerPointRegisterEvent>(OnEnemySpawnerPointRegisterEvent);
            mEventHandlerService.RemoveListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
            mEventHandlerService.RemoveListener<EnemyDeadEvent>(OnEnemyDeadEvent);
        }

        private void OnEnemyDeadEvent(EnemyDeadEvent e)
        {
            HandleEnemyDespawned();
        }

        private void HandleEnemyDespawned()
        {
            mEnemiesAlive -= 1;

            if (mEnemiesAlive == 0 || !mScoreKeeperService.HasScoreLeft())
            {
                mEventHandlerService.TriggerEvent(new LevelCompletedEvent(mScoreKeeperService.HasScoreLeft()));
            }
        }

        private void OnEnemyReachedDestinationEvent(EnemyReachedDestinationEvent e)
        {
            HandleEnemyDespawned();
        }

        private void OnEnemySpawnerPointRegisterEvent(EnemySpawnerPointRegisterEvent e)
        {
            mMaxEnemies = e.MaxEnemies;
            mEnemiesAlive = mMaxEnemies;
        }
    }
}