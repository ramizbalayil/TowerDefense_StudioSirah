using frameworks.services.events;
using towerdefence.systems.spawner;

namespace towerdefence.events
{
    public class HeroSpawnerPointRegisterEvent : AppEvent
    {
        public HeroSpawnPoint HeroSpawnPoint;

        public HeroSpawnerPointRegisterEvent(HeroSpawnPoint heroSpawnPoint)
        {
            HeroSpawnPoint = heroSpawnPoint;
        }
    }
}
