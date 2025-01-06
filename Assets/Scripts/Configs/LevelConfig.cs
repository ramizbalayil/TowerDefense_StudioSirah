using frameworks.configs;
using System.Collections.Generic;
using towerdefence.data;
using UnityEngine;

namespace towerdefence.configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Studio Sirah/Config/Level Config", order = 1)]
    public class LevelConfig : BaseConfig
    {
        public List<LevelInfo> LevelInfos;
    }

    [System.Serializable]
    public class LevelInfo
    {
        public GameObject LevelPrefab;
        public bool Locked;
        public UpgradeReward UpgradeReward;

        public LevelInfo(LevelInfo LevelInfo)
        {
            LevelPrefab = LevelInfo.LevelPrefab;
            Locked = LevelInfo.Locked;
            UpgradeReward = LevelInfo.UpgradeReward;
        }
    }
}