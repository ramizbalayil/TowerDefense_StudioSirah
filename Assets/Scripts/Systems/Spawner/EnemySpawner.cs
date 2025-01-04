using System;
using System.Collections;
using towerdefence.characters.enemy;
using towerdefence.events;
using UnityEngine;

namespace towerdefence.systems.spawner
{
    public class EnemySpawner : Spawner<EnemyBehaviour>
    {
        private Vector3 spawnPoint;

        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<EnemySpawnerPointRegisterEvent>(OnEnemySpawnerPointRegister);
            mEventHandlerService.AddListener<EnemyReachedDestinationEvent>(OnReleaseEnemyEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<EnemySpawnerPointRegisterEvent>(OnEnemySpawnerPointRegister);
            mEventHandlerService.RemoveListener<EnemyReachedDestinationEvent>(OnReleaseEnemyEvent);
        }

        private void OnReleaseEnemyEvent(EnemyReachedDestinationEvent e)
        {
            spawnPool.Release(e.EnemyBehaviour);
        }

        private void OnEnemySpawnerPointRegister(EnemySpawnerPointRegisterEvent e)
        {
            spawnPoint = e.EnemySpawnPoint.transform.position;
            e.EnemySpawnPoint.Hide();

            StartCoroutine(SpawnEnemyUnits(spawnPoint, e.EnemyDestination, e.MaxEnemies, e.SpawnInterval));
        }

        private IEnumerator SpawnEnemyUnits(Vector3 spawnPosition, Vector3 destination, int maxEnemies, float spawnInterval)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                EnemyBehaviour enemy = SpawnUnit(spawnPosition);
                enemy.SetDestination(destination);
                yield return new WaitForSeconds(spawnInterval);
            }

            yield return null;
        }
    }
}
