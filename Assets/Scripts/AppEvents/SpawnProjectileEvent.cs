using frameworks.services.events;
using UnityEngine;

namespace towerdefence.events
{
    public class SpawnProjectileEvent : AppEvent
    {
        public Vector3 SpawnPosition;
        public Vector3 ProjectileDirection;

        public SpawnProjectileEvent(Vector3 spawnPosition, Vector3 projectileDirection)
        {
            SpawnPosition = spawnPosition;
            ProjectileDirection = projectileDirection;
        }
    }
}