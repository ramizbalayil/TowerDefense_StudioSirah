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
            mEventHandlerService.AddListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
            mEventHandlerService.AddListener<EnemyDeadEvent>(OnEnemyDeadEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<EnemySpawnerPointRegisterEvent>(OnEnemySpawnerPointRegister);
            mEventHandlerService.RemoveListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
            mEventHandlerService.RemoveListener<EnemyDeadEvent>(OnEnemyDeadEvent);
        }

        private void OnEnemyDeadEvent(EnemyDeadEvent e)
        {
            spawnPool.Release(e.Enemy);
        }

        private void OnEnemyReachedDestinationEvent(EnemyReachedDestinationEvent e)
        {
            spawnPool.Release(e.EnemyBehaviour);
        }

        private void OnEnemySpawnerPointRegister(EnemySpawnerPointRegisterEvent e)
        {
            spawnPoint = e.EnemySpawnPoint.transform.position;

            StartCoroutine(SpawnEnemyUnits(spawnPoint, e.EnemyDestination, e.MaxEnemies, e.SpawnInterval, e.EnemySpeed));
        }

        private IEnumerator SpawnEnemyUnits(Vector3 spawnPosition, Vector3 destination, int maxEnemies, float spawnInterval, float enemySpeed)
        {
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < maxEnemies; i++)
            {
                EnemyBehaviour enemy = SpawnUnit(spawnPosition);
                enemy.SetAgentEnabled(true);
                enemy.ResetHealth();
                enemy.SetSpeed(enemySpeed);
                enemy.SetDestination(destination);
                enemy.UpdateHealth(1);
                mEventHandlerService.TriggerEvent(new EnemySpawnEvent(enemy));
                yield return new WaitForSeconds(spawnInterval);
            }

            yield return null;
        }

        protected override void ReleaseUnit(EnemyBehaviour enemy)
        {
            enemy.SetAgentEnabled(false);
            base.ReleaseUnit(enemy);
        }

        protected override EnemyBehaviour InitializeUnits()
        {
            EnemyBehaviour enemy = base.InitializeUnits();
            enemy.SetAgentEnabled(false);
            return enemy;
        }
    }
}
