using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using System;
using towerdefence.events;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace towerdefence.systems.spawner
{
    public class HeroSpawnPoint : BaseBehaviour
    {
        [SerializeField] private string HeroID;
        [InjectService] private EventHandlerService mEventHandlerService;

        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<DragHeroSpawnPoint>(OnDragHeroSpawnPoint);
            mEventHandlerService.AddListener<StartGameEvent>(OnStartGameEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<DragHeroSpawnPoint>(OnDragHeroSpawnPoint);
            mEventHandlerService.RemoveListener<StartGameEvent>(OnStartGameEvent);
        }

        private void OnStartGameEvent(StartGameEvent e)
        {
            mEventHandlerService.TriggerEvent(new HeroSpawnerPointRegisterEvent(HeroID, this));
        }

        private void OnDragHeroSpawnPoint(DragHeroSpawnPoint e)
        {
            if (e.IsDragging && e.HeroId == HeroID)
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
