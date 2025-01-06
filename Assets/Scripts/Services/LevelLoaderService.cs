using frameworks.configs;
using frameworks.services;
using System.Collections.Generic;
using towerdefence.configs;
using towerdefence.data;

namespace towerdefence.services
{
    public class LevelLoaderService : BaseService
    {
        private LevelInfo mCachedLevelInfo;
        private int mCachedLevelNumber = 0;

        private List<LevelInfo> mLevelInfos = null;
        private List<LevelInfo> pLevelInfos
        {
            get
            { 
                if (mLevelInfos == null)
                {
                    mLevelInfos = new List<LevelInfo>();
                    List<LevelInfo> infos = ConfigRegistry.Get<LevelConfig>().LevelInfos;
                    foreach (LevelInfo levelInfo in infos)
                    {
                        mLevelInfos.Add(new LevelInfo(levelInfo));
                    }
                }
                return mLevelInfos;
            }
        }

        public List<LevelInfo> GetLevelInfos()
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
            if (mCachedLevelNumber < pLevelInfos.Count)
            {
                LevelInfo info = pLevelInfos[mCachedLevelNumber];
                info.Locked = false;
                pLevelInfos[mCachedLevelNumber] = info;
            }
        }

        public LevelReward GetLevelReward()
        {
            return mCachedLevelInfo.LevelReward;
        }
    }
}