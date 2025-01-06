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

            int availableCards = mHeroRosterService.GetUpgradeCardsFor(heroInfo.HeroID);
            UpgradeLevel upgradeLevel = mHeroInfo.UpgradeLevels[mHeroInfo.Level];
            _CardsRequiredLabel.text = $"[{availableCards} / {upgradeLevel.CardsRequired}]";
            _CardsRequiredSlider.fillAmount = availableCards / upgradeLevel.CardsRequired;

            _UpgradeButton.interactable = availableCards == upgradeLevel.CardsRequired;
        }
    }
}