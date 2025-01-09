using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using frameworks.services.scenemanagement;
using frameworks.ui;
using TMPro;
using towerdefence.events;
using towerdefence.services;
using UnityEngine;
using UnityEngine.UI;

namespace towerdefence.ui
{
    public class UIResultsScreen : UIScreen
    {
        [InjectService] private EventHandlerService mEventHandlerService;
        [InjectService] private SceneManagementService mSceneManagementService;
        [InjectService] private LevelLoaderService mLevelLoaderService;
        [InjectService] private HerosRosterService mHerosRosterService;

        [SerializeField] private TextMeshProUGUI _ResultLabel;
        [SerializeField] private TextMeshProUGUI _RewardsLabel;
        [SerializeField] private Button _BackToLobbyButton;

        protected override void Awake()
        {
            base.Awake();
            _BackToLobbyButton.onClick.AddListener(OnBackToLobbyButtonClicked);
            mEventHandlerService.AddListener<LevelCompletedEvent>(OnLevelCompletedEvent);
        }

        private void OnBackToLobbyButtonClicked()
        {
            mSceneManagementService.LoadSceneAsync(1);
        }

        private void OnDestroy()
        {
            _BackToLobbyButton.onClick.RemoveListener(OnBackToLobbyButtonClicked);
            mEventHandlerService.RemoveListener<LevelCompletedEvent>(OnLevelCompletedEvent);
        }

        private void OnLevelCompletedEvent(LevelCompletedEvent e)
        {
            Show();
            if (e.Won)
            {
                string heroId = mHerosRosterService.GetRandomHeroId();
                mHerosRosterService.AddUpgradeCardsFor(heroId, 1);
                mLevelLoaderService.UnlockNextLevel();

                _ResultLabel.text = "You Won";
                _RewardsLabel.text = $"Reward : 1 Upgrade Card for {heroId}";
            }
            else
            {
                _ResultLabel.text = "You Lost";
                _RewardsLabel.text = "";
            }
        }
    }
}