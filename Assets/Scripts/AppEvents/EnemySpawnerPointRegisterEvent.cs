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
        public float EnemySpeed;
        public EnemySpawnerPointRegisterEvent(EnemySpawnPoint enemySpawnPoint, Vector3 enemyDestination, int maxEnemies, float spawnInterval, float enemySpeed)
        {
            EnemySpawnPoint = enemySpawnPoint;
            EnemyDestination = enemyDestination;
            MaxEnemies = maxEnemies;
            SpawnInterval = spawnInterval;
            EnemySpeed = enemySpeed;
        }
    }
}

