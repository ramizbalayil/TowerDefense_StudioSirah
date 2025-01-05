using frameworks.services.events;
using towerdefence.systems.projectiles;

namespace towerdefence.events
{
    public class ProjectileHitEvent : AppEvent
    {
        public ProjectileBehaviour Projectile;

        public ProjectileHitEvent(ProjectileBehaviour projectile)
        {
            Projectile = projectile;
        }
    }
}

