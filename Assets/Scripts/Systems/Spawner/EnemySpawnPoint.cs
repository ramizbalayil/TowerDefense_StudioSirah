using frameworks.services.events;
using frameworks.services;
using towerdefence.events;
using frameworks.ioc;
using UnityEngine;

namespace towerdefence.systems.spawner
{
    public class EnemySpawnPoint : BaseBehaviour
    {
        [SerializeField] private Transform mDestination;
        [SerializeField] private int mMaxEnemies;
        [SerializeField] private float mSpawnInterval;

        [InjectService] private EventHandlerService mEventHandlerService;

        protected void Start()
        {
            mEventHandlerService.TriggerEvent(new EnemySpawnerPointRegisterEvent(this, mDestination.position, mMaxEnemies, mSpawnInterval));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
