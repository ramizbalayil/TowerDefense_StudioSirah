using frameworks.services;
using frameworks.ui;
using towerdefence.services;
using UnityEngine;
using UnityEngine.UI;

namespace towerdefence.ui
{
    public class UIMainMenu : UIScreen
    {
        [InjectService] HerosRosterService mHeroRosterService;

        [SerializeField] private Button _PlayGameButton;
        [SerializeField] private Button _UpgradeHeroesButton;
        [SerializeField] private GameObject _UpgradeNotification;

        protected override void Awake()
        {
            base.Awake();
            _PlayGameButton.onClick.AddListener(OnPlayGamesButtonClicked);
            _UpgradeHeroesButton.onClick.AddListener(OnUpgradeHeroesButtonClicked);
        }

        private void OnDestroy()
        {
            _PlayGameButton.onClick.RemoveListener(OnPlayGamesButtonClicked);
            _UpgradeHeroesButton.onClick.RemoveListener(OnUpgradeHeroesButtonClicked);
        }

        public override void Show()
        {
            base.Show();
            _UpgradeNotification.SetActive(mHeroRosterService.HasUpgradesAvailable());
        }

        private void OnPlayGamesButtonClicked()
        {
            ScreenManager.Instance.ShowScreen("level_select");
        }

        private void OnUpgradeHeroesButtonClicked()
        {
            ScreenManager.Instance.ShowScreen("upgrade_heroes");
        }
    }
}