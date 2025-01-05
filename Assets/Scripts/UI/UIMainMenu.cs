using frameworks.ui;
using UnityEngine;
using UnityEngine.UI;

namespace towerdefence.ui
{
    public class UIMainMenu : UIScreen
    {
        [SerializeField] private Button _PlayGameButton;
        [SerializeField] private Button _UpgradeHeroesButton;

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