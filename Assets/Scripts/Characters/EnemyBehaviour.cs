using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using towerdefence.ui;
using towerdefence.events;
using towerdefence.services;
using towerdefence.systems.projectiles;
using UnityEngine;
using UnityEngine.AI;

namespace towerdefence.characters.enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyBehaviour : BaseBehaviour
    {
        [InjectService] EventHandlerService mEventHandlerService;
        [InjectService] private ScoreKeeperService mScoreKeeperService;

        [SerializeField] UIHealthBar _HealthBar;
        [SerializeField] Transform _ProjectileTarget;

        private NavMeshAgent mAgent;
        private float mHealth = 100;
        private float mMaxHealth = 100;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            mAgent = GetComponent<NavMeshAgent>();
            mEventHandlerService.AddListener<LevelCompletedEvent>(OnLevelCompletedEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<LevelCompletedEvent>(OnLevelCompletedEvent);
        }

        private void Update()
        {
            if (mAgent.remainingDistance <= 0.01)
            {
                mScoreKeeperService.ReduceScore();
                mEventHandlerService.TriggerEvent(new EnemyReachedDestinationEvent(this));
            }
        }
        #endregion

        #region Health
        public Vector3 GetTargetPosition()
        {
            return _ProjectileTarget.position;
        }
        public void UpdateHealth(float healthPerc)
        {
            _HealthBar.UpdateUIHealth(healthPerc);
        }

        public void ResetHealth()
        {
            mHealth = mMaxHealth;
            _HealthBar.UpdateUIHealth(1);
        }

        private void DoDamage(float damage)
        {
            mHealth -= damage;
            _HealthBar.UpdateUIHealth(mHealth / mMaxHealth);
            CheckIfEnemyIsDead();
        }

        private void CheckIfEnemyIsDead()
        {
            if (mHealth <= 0)
            {
                mEventHandlerService.TriggerEvent(new EnemyDeadEvent(this));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ProjectileBehaviour projectile))
            {
                DoDamage(projectile.GetDamage());
                projectile.HandleProjectileHit();
            }
        }
        #endregion

        #region Navigation
        public void SetDestination(Vector3 target)
        {
            mAgent.SetDestination(target);
        }
        public void SetSpeed(float speed)
        {
            mAgent.speed = speed;
        }

        private void OnLevelCompletedEvent(LevelCompletedEvent e)
        {
            if (mAgent.isOnNavMesh)
            {
                mAgent.isStopped = true;
                SetSpeed(0);
            }
        }
        #endregion
    }
}