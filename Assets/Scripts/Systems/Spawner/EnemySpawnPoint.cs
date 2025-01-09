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
        [SerializeField] private float _EnemySpeed;

        [InjectService] private EventHandlerService mEventHandlerService;

        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<StartGameEvent>(OnStartGameEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<StartGameEvent>(OnStartGameEvent);
        }

        private void OnStartGameEvent(StartGameEvent e)
        {
            mEventHandlerService.TriggerEvent(new EnemySpawnerPointRegisterEvent(this, _Destination.position, _MaxEnemies, _SpawnInterval, _EnemySpeed));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
