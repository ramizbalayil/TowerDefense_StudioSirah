using towerdefence.events;
using towerdefence.systems.spawner;

namespace towerdefence.systems.projectiles
{
    public class ProjectileSpawner : Spawner<ProjectileBehaviour>
    {
        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<SpawnProjectileEvent>(OnSpawnProjectileEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<SpawnProjectileEvent>(OnSpawnProjectileEvent);
        }

        private void OnSpawnProjectileEvent(SpawnProjectileEvent e)
        {
            ProjectileBehaviour projectile = SpawnUnit(e.SpawnPosition);
            projectile.SetDirection(e.ProjectileDirection);
        }
    }
}