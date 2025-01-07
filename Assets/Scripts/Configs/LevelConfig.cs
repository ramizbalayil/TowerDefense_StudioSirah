using frameworks.configs;
using System.Collections.Generic;
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

        public LevelInfo(LevelInfo LevelInfo)
        {
            LevelPrefab = LevelInfo.LevelPrefab;
            Locked = LevelInfo.Locked;
        }
    }
}