using frameworks.services;
using towerdefence.characters.hero;
using towerdefence.configs;
using towerdefence.events;
using towerdefence.services;

namespace towerdefence.systems.spawner
{
    public class HeroSpawner : Spawner<HeroBehaviour>
    {
        [InjectService] private HerosRosterService mHeroRosterService;

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
            UpgradeLevel upgradeLevel = mHeroRosterService.GetCurrentLevelStats(e.HeroID);
            HeroBehaviour heroBehaviour = SpawnUnit(e.HeroSpawnPoint.transform.position);
            heroBehaviour.SetHeroStats(upgradeLevel.ProjectileSpawnInterval, upgradeLevel.EnemyReachRadius);
            heroBehaviour.SetModel(mHeroRosterService.GetHeroInfo(e.HeroID).HeroPrefab);
            e.HeroSpawnPoint.Hide();
        }
    }
}