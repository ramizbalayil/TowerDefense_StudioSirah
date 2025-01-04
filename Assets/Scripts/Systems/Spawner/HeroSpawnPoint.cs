using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using towerdefence.events;

namespace towerdefence.systems.spawner
{
    public class HeroSpawnPoint : BaseBehaviour
    {
        [InjectService] private EventHandlerService mEventHandlerService;

        protected void Start()
        {
            mEventHandlerService.TriggerEvent(new HeroSpawnerPointRegisterEvent(this));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
