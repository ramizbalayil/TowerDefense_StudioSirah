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

        public UpgradeLevel[] UpgradeLevels;
    }

    [System.Serializable]
    public class UpgradeLevel
    {
        public int CardsRequired;
        public float EnemyReachRadius;
        public float ProjectileSpawnInterval;
    }
}