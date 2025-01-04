using frameworks.services.events;
using frameworks.services;
using towerdefence.events;
using frameworks.ioc;
using UnityEngine;

namespace towerdefence.systems.spawner
{
    public class EnemySpawnPoint : BaseBehaviour
    {
        [SerializeField] private Transform _Destination;
        [SerializeField] private int _MaxEnemies;
        [SerializeField] private float _SpawnInterval;

        [InjectService] private EventHandlerService mEventHandlerService;

        protected void Start()
        {
            mEventHandlerService.TriggerEvent(new EnemySpawnerPointRegisterEvent(this, _Destination.position, _MaxEnemies, _SpawnInterval));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
