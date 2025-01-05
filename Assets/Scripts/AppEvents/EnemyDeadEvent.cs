using frameworks.services.events;
using towerdefence.characters.enemy;

namespace towerdefence.events
{
    public class EnemyDeadEvent : AppEvent
    {
        public EnemyBehaviour Enemy;
        public EnemyDeadEvent(EnemyBehaviour enemy)
        {
            Enemy = enemy;
        }
    }
}