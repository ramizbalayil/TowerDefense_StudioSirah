using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using towerdefence.events;
using UnityEngine;

namespace towerdefence.systems.projectiles
{
    public class ProjectileBehaviour : BaseBehaviour
    {
        [InjectService] private EventHandlerService mEventHandlerService;

        [SerializeField] private float mDamage = 10f;
        [SerializeField] private float mSpeed = 20f;

        private Vector3 mDirection = Vector3.zero;

        public void SetDirection(Vector3 direction)
        {
            mDirection = direction;
        }

        public float GetDamage()
        {
            return mDamage;
        }

        private void Update()
        {
            transform.Translate(mSpeed * Time.deltaTime * mDirection);
        }

        public void HandleProjectileHit()
        {
            mEventHandlerService.TriggerEvent(new ProjectileHitEvent(this));
        }
    }
}
