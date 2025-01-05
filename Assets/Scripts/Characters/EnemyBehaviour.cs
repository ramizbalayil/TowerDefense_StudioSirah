using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using towerdefence.characters.health;
using towerdefence.events;
using towerdefence.systems.projectiles;
using UnityEngine;
using UnityEngine.AI;

namespace towerdefence.characters.enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyBehaviour : BaseBehaviour
    {
        [InjectService] EventHandlerService mEventHandlerService;
        
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
        }
        private void Update()
        {
            if (mAgent.remainingDistance <= 0.01)
            {
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
        #endregion
    }
}