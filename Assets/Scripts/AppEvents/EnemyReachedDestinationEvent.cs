using frameworks.services.events;
using towerdefence.characters.enemy;

namespace towerdefence.events
{
    public class EnemyReachedDestinationEvent : AppEvent
    {
        public EnemyBehaviour EnemyBehaviour;

        public EnemyReachedDestinationEvent(EnemyBehaviour enemyBehaviour)
        {
            EnemyBehaviour = enemyBehaviour;
        }
    }
}

