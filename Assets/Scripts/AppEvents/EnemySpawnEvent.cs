using frameworks.services.events;
using towerdefence.characters.enemy;

namespace towerdefence.events
{
    public class EnemySpawnEvent : AppEvent
    {
        public EnemyBehaviour Enemy;

        public EnemySpawnEvent(EnemyBehaviour enemy)
        {
            Enemy = enemy;
        }
    }
}
