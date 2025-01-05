using frameworks.configs;
using frameworks.services;
using towerdefence.configs;

namespace towerdefence.services
{
    public class LevelLoaderService : BaseService
    {
        private LevelInfo mCachedLevelInfo;
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
        }

        public LevelInfo UnloadLevelInfo()
        {
            LevelInfo info = mCachedLevelInfo;
            mCachedLevelInfo = null;
            return info;
        }
    }
}