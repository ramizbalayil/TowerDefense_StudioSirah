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
        [SerializeField] private GameObject _SpawnPrefab;

        [SerializeField] private int _DefaultPoolSize = 3;
        [SerializeField] private int _MaxPoolSize = 5;

        [InjectService] protected EventHandlerService mEventHandlerService;

        protected IObjectPool<T> spawnPool;

        protected override void Awake()
        {
            base.Awake();
            spawnPool = new ObjectPool<T>(InitializeUnits, GetUnit, ReleaseUnit,
                DestroyUnit, false, _DefaultPoolSize, _MaxPoolSize);
        }

        protected virtual T SpawnUnit(Vector3 pos)
        {
            T unit = spawnPool.Get();
            unit.transform.position = new Vector3(pos.x, pos.y, pos.z);
            return unit;
        }

        protected virtual T InitializeUnits()
        {
            GameObject go = Instantiate(_SpawnPrefab, transform);
            return go.GetComponent<T>();
        }

        protected virtual void DestroyUnit(T go)
        {
            Destroy(go.gameObject);
        }

        protected virtual void ReleaseUnit(T go)
        {
            go.gameObject.SetActive(false);
        }

        protected virtual void GetUnit(T go)
        {
            go.gameObject.SetActive(true);
        }
    }
}