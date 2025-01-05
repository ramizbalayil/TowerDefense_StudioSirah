using frameworks.ioc;
using frameworks.services.events;
using frameworks.services;
using UnityEngine;
using towerdefence.events;
using System.Collections.Generic;
using towerdefence.characters.enemy;

namespace towerdefence.characters.hero
{
    public class HeroBehaviour : BaseBehaviour
    {
        [SerializeField] private Transform _ProjectileSpawnPoint;
        [SerializeField] private float _EnemyReachRadius = 5f;
        [SerializeField] private float _ProjectileSpawnInterval = 1f;

        [InjectService] protected EventHandlerService mEventHandlerService;

        private List<EnemyBehaviour> mEnemies;
        private EnemyBehaviour mCurrentTargetEnemy;
        private float timeElapsed = 0f;

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

            if (mCurrentTargetEnemy != null)
                ShootCurrentTargetEnemy();
        }

        private void ShootCurrentTargetEnemy()
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > _ProjectileSpawnInterval)
                ShootTowardsProjectileInIntervals();
        }

        private void ShootTowardsProjectileInIntervals()
        {
            timeElapsed = 0f;
            Vector3 projectileDirection = (mCurrentTargetEnemy.GetTargetPosition() - _ProjectileSpawnPoint.position).normalized;
            mEventHandlerService.TriggerEvent(new SpawnProjectileEvent(_ProjectileSpawnPoint.position, projectileDirection));
        }

        private void CheckIfCurrentTargetEnemyIsStillInRange()
        {
            if (Vector3.Distance(transform.position, mCurrentTargetEnemy.transform.position) >= _EnemyReachRadius)
            {
                mCurrentTargetEnemy = null;
                timeElapsed = 0f;
            }
        }

        private void CheckForEnemiesCloseBy()
        {
            foreach (EnemyBehaviour enemy in mEnemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < _EnemyReachRadius)
                {
                    mCurrentTargetEnemy = enemy;
                    timeElapsed = 0f;

                    break;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _EnemyReachRadius);

            Gizmos.color = Color.yellow;
            foreach (EnemyBehaviour enemy in mEnemies)
            {
                if (mCurrentTargetEnemy != enemy)
                    Gizmos.DrawLine(transform.position, enemy.GetTargetPosition());
            }

            Gizmos.color = Color.red;
            if (mCurrentTargetEnemy != null)
                Gizmos.DrawLine(transform.position, mCurrentTargetEnemy.GetTargetPosition());
        }
    }
}