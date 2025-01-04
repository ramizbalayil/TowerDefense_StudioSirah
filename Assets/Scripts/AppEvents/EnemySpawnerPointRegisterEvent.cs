using frameworks.services.events;
using towerdefence.systems.spawner;
using UnityEngine;

namespace towerdefence.events
{
    public class EnemySpawnerPointRegisterEvent : AppEvent
    {
        public EnemySpawnPoint EnemySpawnPoint;
        public Vector3 EnemyDestination;
        public int MaxEnemies;
        public float SpawnInterval;

        public EnemySpawnerPointRegisterEvent(EnemySpawnPoint enemySpawnPoint, Vector3 enemyDestination, int maxEnemies, float spawnInterval)
        {
            EnemySpawnPoint = enemySpawnPoint;
            EnemyDestination = enemyDestination;
            MaxEnemies = maxEnemies;
            SpawnInterval = spawnInterval;
        }
    }
}

