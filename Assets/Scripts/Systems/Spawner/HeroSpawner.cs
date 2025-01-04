using towerdefence.characters.hero;
using towerdefence.events;
using UnityEngine;

namespace towerdefence.systems.spawner
{
    public class HeroSpawner : Spawner<HeroBehaviour>
    {
        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<HeroSpawnerPointRegisterEvent>(OnHeroSpawnerPointRegister);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<HeroSpawnerPointRegisterEvent>(OnHeroSpawnerPointRegister);
        }

        public void OnHeroSpawnerPointRegister(HeroSpawnerPointRegisterEvent e)
        {
            SpawnUnit(e.HeroSpawnPoint.transform.position);
            e.HeroSpawnPoint.Hide();
        }
    }
}