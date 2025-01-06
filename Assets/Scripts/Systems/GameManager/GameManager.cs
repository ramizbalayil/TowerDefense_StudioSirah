using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using System;
using towerdefence.data;
using towerdefence.events;
using towerdefence.services;

namespace towerdefence.systems.manager
{
    public class GameManager : BaseBehaviour
    {
        [InjectService] private EventHandlerService mEventHandlerService;
        [InjectService] private ScoreKeeperService mScoreKeeperService;
        [InjectService] private HerosRosterService mHerosRosterService;
        [InjectService] private LevelLoaderService mLevelLoaderService;

        private int mMaxEnemies;
        private int mEnemiesAlive;

        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<EnemySpawnerPointRegisterEvent>(OnEnemySpawnerPointRegisterEvent);
            mEventHandlerService.AddListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
            mEventHandlerService.AddListener<EnemyDeadEvent>(OnEnemyDeadEvent);
            mEventHandlerService.AddListener<LevelCompletedEvent>(OnLevelCompletedEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<EnemySpawnerPointRegisterEvent>(OnEnemySpawnerPointRegisterEvent);
            mEventHandlerService.RemoveListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
            mEventHandlerService.RemoveListener<EnemyDeadEvent>(OnEnemyDeadEvent);
            mEventHandlerService.RemoveListener<LevelCompletedEvent>(OnLevelCompletedEvent);
        }

        private void OnLevelCompletedEvent(LevelCompletedEvent e)
        {
            if (e.Won)
            {
                HandleLevelWin();
            }
        }

        private void OnEnemyDeadEvent(EnemyDeadEvent e)
        {
            HandleEnemyDespawned();
        }

        private void HandleEnemyDespawned()
        {
            mEnemiesAlive -= 1;

            if (mEnemiesAlive == 0)
            {
                mEventHandlerService.TriggerEvent(new LevelCompletedEvent(mScoreKeeperService.HasScoreLeft()));
            }
        }
        private void HandleLevelWin()
        {
            mLevelLoaderService.UnlockNextLevel();

            UpgradeReward upgradeReward = mLevelLoaderService.GetLevelReward();
            mHerosRosterService.AddUpgradeCardsFor(upgradeReward.HeroId, upgradeReward.RewardCards);
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