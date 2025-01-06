using frameworks.configs;
using frameworks.inventory;
using frameworks.services;
using System.Collections.Generic;
using System.Threading.Tasks;
using towerdefence.configs;

namespace towerdefence.services
{
    public class HerosRosterService : BaseService
    {
        private List<HeroInfo> mHeroInfos = null;
        private List<HeroInfo> pHeroInfos
        {
            get
            {
                if (mHeroInfos == null)
                {
                    mHeroInfos = new List<HeroInfo>(ConfigRegistry.Get<HeroConfig>().HeroInfos);
                }
                return mHeroInfos;
            }
        }

        private BaseInventory<int> mUpgradeCardInventory;

        public override Task Init()
        {
            mUpgradeCardInventory = new BaseInventory<int>();
            return base.Init();
        }

        public List<HeroInfo> GetHeroInfos()
        {
            return pHeroInfos;
        }

        public HeroInfo UpgradeHero(string heroId)
        {
            for (int i = 0; i < mHeroInfos.Count; i++)
            {
                HeroInfo heroInfo = mHeroInfos[i];
                if (heroInfo.HeroID == heroId)
                {
                    heroInfo.Level += 1;
                    return heroInfo;
                }
            }
            return null;
        }

        public UpgradeLevel GetCurrentLevelStats(string heroId)
        {
            for (int i = 0; i < mHeroInfos.Count; i++)
            {
                HeroInfo heroInfo = mHeroInfos[i];
                if (heroInfo.HeroID == heroId)
                {
                    return heroInfo.UpgradeLevels[heroInfo.Level - 1];
                }
            }
            return null;
        }

        public int GetUpgradeCardsFor(string heroId)
        {
            return mUpgradeCardInventory.GetItem(heroId);
        }

        public void AddUpgradeCardsFor(string heroId, int amount)
        {
            int prevAmount = mUpgradeCardInventory.GetItem(heroId);
            mUpgradeCardInventory.SetItem(heroId, prevAmount + amount);
        }

        public void ConsumeUpgradeCardsFor(string heroId, int amount)
        {
            int prevAmount = mUpgradeCardInventory.GetItem(heroId);
            mUpgradeCardInventory.SetItem(heroId, prevAmount - amount);
        }
    }
}