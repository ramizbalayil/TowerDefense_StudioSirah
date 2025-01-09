using frameworks.ioc;
using frameworks.services.events;
using frameworks.services;
using UnityEngine;
using towerdefence.events;
using System.Collections.Generic;
using towerdefence.characters.enemy;
using towerdefence.ui;
using UnityEngine.UI;

namespace towerdefence.characters.hero
{
    public class HeroBehaviour : BaseBehaviour
    {
        [SerializeField] private Transform _ProjectileSpawnPoint;
        [SerializeField] private UIReachRadius _ReachRadius;
        [SerializeField] private Image _ProjectileSpawnTimer;

        [InjectService] protected EventHandlerService mEventHandlerService;

        private List<EnemyBehaviour> mEnemies;
        private EnemyBehaviour mCurrentTargetEnemy;
        private float mTimeElapsed = 0f;
        private float mEnemyReachRadius = 1f;
        private float mProjectileSpawnInterval = 1f;

        protected override void Awake()
        {
            base.Awake();
            mEnemies = new List<EnemyBehaviour>();
            mEventHandlerService.AddListener<EnemySpawnEvent>(OnEnemySpawnEvent);
            mEventHandlerService.AddListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
            mEventHandlerService.AddListener<EnemyDeadEvent>(OnEnemyDeadEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<EnemySpawnEvent>(OnEnemySpawnEvent);
            mEventHandlerService.RemoveListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
            mEventHandlerService.RemoveListener<EnemyDeadEvent>(OnEnemyDeadEvent);
        }

        public void SetHeroStats(float projectileSpawnInterval, float enemyReachRadius)
        {
            mProjectileSpawnInterval = projectileSpawnInterval;
            mEnemyReachRadius = enemyReachRadius;
            _ReachRadius.SetSize(mEnemyReachRadius);
            mTimeElapsed = mProjectileSpawnInterval;
            UpdateProjectileTimer();
        }

        private void UpdateProjectileTimer()
        {
            float perc = 0f;
            if (mTimeElapsed <= mProjectileSpawnInterval)
            {
                perc = mProjectileSpawnInterval - mTimeElapsed;
            }
            _ProjectileSpawnTimer.fillAmount = perc / mProjectileSpawnInterval;
        }

        public void SetModel(GameObject model)
        {
            Instantiate(model, transform);
        }

        private void OnEnemyDeadEvent(EnemyDeadEvent e)
        {
            HandleEnemyDespawned(e.Enemy);
        }

        private void HandleEnemyDespawned(EnemyBehaviour enemy)
        {
            if (mCurrentTargetEnemy == enemy)
                mCurrentTargetEnemy = null;

            if (mEnemies.Contains(enemy))
                mEnemies.Remove(enemy);
        }

        private void OnEnemyReachedDestinationEvent(EnemyReachedDestinationEvent e)
        {
            HandleEnemyDespawned(e.EnemyBehaviour);
        }

        private void OnEnemySpawnEvent(EnemySpawnEvent e)
        {
            mEnemies.Add(e.Enemy);
        }

        private void Update()
        {
            if (mCurrentTargetEnemy != null)
                CheckIfCurrentTargetEnemyIsStillInRange();

            if (mCurrentTargetEnemy == null)
                CheckForEnemiesCloseBy();

            if (mCurrentTargetEnemy == null && mTimeElapsed < mProjectileSpawnInterval)
                IncrementRunningTimer();

            if (mCurrentTargetEnemy != null)
                ShootCurrentTargetEnemy();
        }

        private void ShootCurrentTargetEnemy()
        {
            IncrementRunningTimer();

            if (mTimeElapsed > mProjectileSpawnInterval)
                ShootTowardsProjectileInIntervals();
        }

        private void IncrementRunningTimer()
        {
            mTimeElapsed += Time.deltaTime;
            UpdateProjectileTimer();
        }

        private void ShootTowardsProjectileInIntervals()
        {
            mTimeElapsed = 0f;
            UpdateProjectileTimer();

            Vector3 projectileDirection = (mCurrentTargetEnemy.GetTargetPosition() - _ProjectileSpawnPoint.position).normalized;
            mEventHandlerService.TriggerEvent(new SpawnProjectileEvent(_ProjectileSpawnPoint.position, projectileDirection));
        }

        private void CheckIfCurrentTargetEnemyIsStillInRange()
        {
            if (Vector3.Distance(transform.position, mCurrentTargetEnemy.transform.position) >= mEnemyReachRadius)
            {
                mCurrentTargetEnemy = null;
            }
        }

        private void CheckForEnemiesCloseBy()
        {
            foreach (EnemyBehaviour enemy in mEnemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < mEnemyReachRadius)
                {
                    mCurrentTargetEnemy = enemy;
                    break;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, mEnemyReachRadius);

            Gizmos.color = Color.yellow;

            if (mEnemies != null &&  mEnemies.Count > 0)
            {
                foreach (EnemyBehaviour enemy in mEnemies)
                {
                    if (mCurrentTargetEnemy != enemy)
                        Gizmos.DrawLine(transform.position, enemy.GetTargetPosition());
                }
            }

            Gizmos.color = Color.red;
            if (mCurrentTargetEnemy != null)
                Gizmos.DrawLine(transform.position, mCurrentTargetEnemy.GetTargetPosition());
        }
    }
}