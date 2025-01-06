using frameworks.configs;
using System.Collections.Generic;
using UnityEngine;

namespace towerdefence.configs
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Studio Sirah/Config/Hero Config", order = 1)]
    public class HeroConfig : BaseConfig
    {
        public List<HeroInfo> HeroInfos;
    }

    [System.Serializable]
    public class HeroInfo
    {
        public string HeroID;
        public GameObject HeroPrefab;
        public int Level;
        public GameObject CharacterPreviewPrefab;
        public Texture CharacterPreviewRenderTexture;

        public List<UpgradeLevel> UpgradeLevels;

        public HeroInfo(HeroInfo HeroInfo)
        {
            HeroID = HeroInfo.HeroID;
            HeroPrefab = HeroInfo.HeroPrefab;
            Level = HeroInfo.Level;
            CharacterPreviewPrefab = HeroInfo.CharacterPreviewPrefab;
            UpgradeLevels = new List<UpgradeLevel>();

            foreach (UpgradeLevel UpgradeLevel in HeroInfo.UpgradeLevels)
            {
                UpgradeLevels.Add(new UpgradeLevel(UpgradeLevel));
            }
        }
    }

    [System.Serializable]
    public class UpgradeLevel
    {
        public int CardsRequired;
        public float EnemyReachRadius;
        public float ProjectileSpawnInterval;

        public UpgradeLevel(UpgradeLevel UpgradeLevel)
        {
            CardsRequired = UpgradeLevel.CardsRequired;
            EnemyReachRadius = UpgradeLevel.EnemyReachRadius;
            ProjectileSpawnInterval = UpgradeLevel.ProjectileSpawnInterval;
        }
    }
}