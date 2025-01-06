using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using towerdefence.events;
using UnityEngine;

namespace towerdefence.systems.spawner
{
    public class HeroSpawnPoint : BaseBehaviour
    {
        [SerializeField] private string HeroID;
        [InjectService] private EventHandlerService mEventHandlerService;

        protected void Start()
        {
            mEventHandlerService.TriggerEvent(new HeroSpawnerPointRegisterEvent(HeroID, this));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
