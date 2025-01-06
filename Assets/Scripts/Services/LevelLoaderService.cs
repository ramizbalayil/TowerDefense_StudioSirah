using frameworks.configs;
using frameworks.services;
using towerdefence.configs;
using towerdefence.data;

namespace towerdefence.services
{
    public class LevelLoaderService : BaseService
    {
        private LevelInfo mCachedLevelInfo;
        private int mCachedLevelNumber = 0;

        private LevelInfo[] mLevelInfos = null;
        private LevelInfo[] pLevelInfos
        {
            get
            { 
                if (mLevelInfos == null)
                {
                    mLevelInfos = ConfigRegistry.Get<LevelConfig>().LevelInfos;
                }
                return mLevelInfos;
            }
        }

        public LevelInfo[] GetLevelInfos()
        {
            return pLevelInfos;
        }

        public void LoadLevelInfo(int level)
        {
            mCachedLevelInfo = pLevelInfos[level - 1];
            mCachedLevelNumber = level;
        }

        public LevelInfo GetLevelInfo()
        {
            return mCachedLevelInfo;
        }

        public void UnlockNextLevel()
        {
            if (mCachedLevelNumber < pLevelInfos.Length)
            {
                LevelInfo info = pLevelInfos[mCachedLevelNumber];
                info.Locked = false;
                pLevelInfos[mCachedLevelNumber] = info;
            }
        }

        public UpgradeReward GetLevelReward()
        {
            return mCachedLevelInfo.UpgradeReward;
        }
    }
}