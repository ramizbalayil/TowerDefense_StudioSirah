using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using towerdefence.events;
using UnityEngine;
using UnityEngine.AI;

namespace towerdefence.characters.enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyBehaviour : BaseBehaviour
    {
        [InjectService] EventHandlerService mEventHandlerService;

        private NavMeshAgent mAgent;

        protected override void Awake()
        {
            base.Awake();
            mAgent = GetComponent<NavMeshAgent>();
        }

        public void SetDestination(Vector3 target)
        {
            mAgent.SetDestination(target);
        }

        private void Update()
        {
            if (mAgent.remainingDistance <= 0.01)
            {
                mEventHandlerService.TriggerEvent(new EnemyReachedDestinationEvent(this));
            }
        }
    }
}