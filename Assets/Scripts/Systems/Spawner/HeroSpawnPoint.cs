using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using towerdefence.configs;
using towerdefence.events;
using towerdefence.services;
using towerdefence.ui;
using UnityEngine;
using UnityEngine.AI;

namespace towerdefence.systems.spawner
{
    public class HeroSpawnPoint : BaseBehaviour
    {
        [SerializeField] private string _HeroID;
        [SerializeField] private UIReachRadius _ReachRadius;

        [InjectService] private EventHandlerService mEventHandlerService;
        [InjectService] private HerosRosterService mHeroRosterService;

        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<DragHeroSpawnPoint>(OnDragHeroSpawnPoint);
            mEventHandlerService.AddListener<StartGameEvent>(OnStartGameEvent);

            SetupEnemyReachRadius();
        }

        private void SetupEnemyReachRadius()
        {
            UpgradeLevel upgradeLevel = mHeroRosterService.GetCurrentLevelStats(_HeroID);
            _ReachRadius.SetSize(upgradeLevel.EnemyReachRadius);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<DragHeroSpawnPoint>(OnDragHeroSpawnPoint);
            mEventHandlerService.RemoveListener<StartGameEvent>(OnStartGameEvent);
        }

        private void OnStartGameEvent(StartGameEvent e)
        {
            mEventHandlerService.TriggerEvent(new HeroSpawnerPointRegisterEvent(_HeroID, this));
        }

        private void OnDragHeroSpawnPoint(DragHeroSpawnPoint e)
        {
            if (e.IsDragging && e.HeroId == _HeroID)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (NavMesh.SamplePosition(hitInfo.point, out NavMeshHit navMeshHit, 1f, NavMesh.AllAreas))
                    {
                        gameObject.transform.position = navMeshHit.position;
                    }
                }
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
