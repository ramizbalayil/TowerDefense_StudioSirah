using System;
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
            mEventHandlerService.AddListener<ProjectileHitEvent>(OnProjectileHitEvent);

        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<SpawnProjectileEvent>(OnSpawnProjectileEvent);
            mEventHandlerService.RemoveListener<ProjectileHitEvent>(OnProjectileHitEvent);
        }

        private void OnProjectileHitEvent(ProjectileHitEvent e)
        {
            spawnPool.Release(e.Projectile);
        }

        private void OnSpawnProjectileEvent(SpawnProjectileEvent e)
        {
            ProjectileBehaviour projectile = SpawnUnit(e.SpawnPosition);
            projectile.SetDirection(e.ProjectileDirection);
        }
    }
}