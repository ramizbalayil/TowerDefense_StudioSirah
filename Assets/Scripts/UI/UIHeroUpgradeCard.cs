using frameworks.ioc;
using frameworks.services;
using TMPro;
using towerdefence.configs;
using towerdefence.services;
using UnityEngine;
using UnityEngine.UI;

namespace towerdefence.ui
{
    public class UIHeroUpgradeCard : BaseBehaviour
    {
        [InjectService] private HerosRosterService mHeroRosterService;

        [SerializeField] private TextMeshProUGUI _LevelInfo;
        [SerializeField] private TextMeshProUGUI _CardsRequiredLabel;
        [SerializeField] private Button _UpgradeButton;
        [SerializeField] private Image _CardsRequiredSlider;
        [SerializeField] private RawImage _CharacterPreview;
        [SerializeField] private TextMeshProUGUI _UpgradeInfo;

        private HeroInfo mHeroInfo;

        protected override void Awake()
        {
            base.Awake();
            _UpgradeButton.onClick.AddListener(OnUpgradePressed);
        }


        private void OnDestroy()
        {
            _UpgradeButton.onClick.RemoveListener(OnUpgradePressed);
        }

        private void OnUpgradePressed()
        {
            UpgradeLevel upgradeLevel = mHeroInfo.UpgradeLevels[mHeroInfo.Level];
            mHeroRosterService.ConsumeUpgradeCardsFor(mHeroInfo.HeroID, upgradeLevel.CardsRequired);
            HeroInfo heroInfo = mHeroRosterService.UpgradeHero(mHeroInfo.HeroID);
            InitializeCard(heroInfo);
        }

        public void InitializeCard(HeroInfo heroInfo)
        {
            mHeroInfo = heroInfo;

            _LevelInfo.text = $"Level {mHeroInfo.Level}";

            _CharacterPreview.texture = mHeroInfo.CharacterPreviewRenderTexture;

            if (mHeroInfo.Level < mHeroInfo.UpgradeLevels.Count)
            {
                int availableCards = mHeroRosterService.GetUpgradeCardsFor(mHeroInfo.HeroID);
                UpgradeLevel currentLevel = mHeroInfo.UpgradeLevels[mHeroInfo.Level - 1];
                UpgradeLevel upgradeLevel = mHeroInfo.UpgradeLevels[mHeroInfo.Level];
                _CardsRequiredLabel.text = $"[{availableCards} / {upgradeLevel.CardsRequired}]";
                _CardsRequiredSlider.fillAmount = availableCards / upgradeLevel.CardsRequired;

                _UpgradeButton.interactable = availableCards >= upgradeLevel.CardsRequired;

                bool hasUpgradeForProjectile = upgradeLevel.ProjectileSpawnInterval < currentLevel.ProjectileSpawnInterval;
                bool hasUpgradeForEnemyReach = upgradeLevel.EnemyReachRadius > currentLevel.EnemyReachRadius;

                _UpgradeInfo.text = "";

                if (hasUpgradeForProjectile)
                    _UpgradeInfo.text += $"-{currentLevel.ProjectileSpawnInterval - upgradeLevel.ProjectileSpawnInterval} Shoot Projectile Interval";

                if (hasUpgradeForProjectile && hasUpgradeForEnemyReach)
                    _UpgradeInfo.text += $"\r\n";

                if (hasUpgradeForEnemyReach)
                    _UpgradeInfo.text += $"+{upgradeLevel.EnemyReachRadius - currentLevel.EnemyReachRadius} Enemy Reach Radius";

            }
            else
            {
                _CardsRequiredLabel.text = $"MAX LEVEL";
                _CardsRequiredSlider.fillAmount = 1;
                _UpgradeButton.interactable = false;
                _UpgradeInfo.text = "";
            }
        }
    }
}