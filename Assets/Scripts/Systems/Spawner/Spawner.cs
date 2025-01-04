using frameworks.ioc;
using frameworks.services.events;
using frameworks.services;
using UnityEngine;
using UnityEngine.Pool;

namespace towerdefence.systems.spawner
{
    public class Spawner<T> : BaseBehaviour where T : MonoBehaviour
    {
        [Header("Spawner Config")]
        [SerializeField] public GameObject SpawnPrefab;

        [SerializeField] private bool collectionCheckForPool = false;
        [SerializeField] private int defaultPoolSize = 3;
        [SerializeField] private int maxPoolSize = 5;

        [InjectService] protected EventHandlerService mEventHandlerService;

        protected IObjectPool<T> spawnPool;

        protected override void Awake()
        {
            base.Awake();
            spawnPool = new ObjectPool<T>(InitializeUnits, GetUnit, ReleaseUnit,
                DestroyUnit, collectionCheckForPool, defaultPoolSize, maxPoolSize);
        }

        protected T SpawnUnit(Vector3 pos)
        {
            T unit = spawnPool.Get();
            unit.transform.position = new Vector3(pos.x, pos.y, pos.z);
            return unit;
        }

        private T InitializeUnits()
        {
            GameObject go = Instantiate(SpawnPrefab, transform);
            return go.GetComponent<T>();
        }
        private void DestroyUnit(T go)
        {
            Destroy(go.gameObject);
        }

        private void ReleaseUnit(T go)
        {
            go.gameObject.SetActive(false);
        }

        private void GetUnit(T go)
        {
            go.gameObject.SetActive(true);
        }
    }
}