using frameworks.services.events;
using towerdefence.systems.spawner;

namespace towerdefence.events
{
    public class HeroSpawnerPointRegisterEvent : AppEvent
    {
        public string HeroID;
        public HeroSpawnPoint HeroSpawnPoint;

        public HeroSpawnerPointRegisterEvent(string heroID, HeroSpawnPoint heroSpawnPoint)
        {
            HeroID = heroID;
            HeroSpawnPoint = heroSpawnPoint;
        }
    }
}
