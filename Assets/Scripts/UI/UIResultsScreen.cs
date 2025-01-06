using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using frameworks.services.scenemanagement;
using TMPro;
using towerdefence.data;
using towerdefence.events;
using towerdefence.services;
using UnityEngine;
using UnityEngine.UI;

namespace towerdefence.ui
{
    public class UIResultsScreen : BaseBehaviour
    {
        [InjectService] private EventHandlerService mEventHandlerService;
        [InjectService] private SceneManagementService mSceneManagementService;
        [InjectService] private LevelLoaderService mLevelLoaderService;

        [SerializeField] private TextMeshProUGUI _ResultLabel;
        [SerializeField] private TextMeshProUGUI _RewardsLabel;
        [SerializeField] private Button _BackToLobbyButton;

        private CanvasGroup mCanvasGroup;

        protected override void Awake()
        {
            base.Awake();
            mCanvasGroup = GetComponent<CanvasGroup>();
            _BackToLobbyButton.onClick.AddListener(OnBackToLobbyButtonClicked);
            mEventHandlerService.AddListener<LevelCompletedEvent>(OnLevelCompletedEvent);
            Hide();
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
                LevelReward levelReward = mLevelLoaderService.GetLevelReward();
                _ResultLabel.text = "You Won";
                _RewardsLabel.text = $"Reward : {levelReward.RewardCards} Upgrade Card for {levelReward.HeroId}";
            }
            else
            {
                _ResultLabel.text = "You Lost";
                _RewardsLabel.text = "";
            }
        }



        private void Hide()
        {
            mCanvasGroup.alpha = 0;
        }

        private void Show()
        {
            mCanvasGroup.alpha = 1;
        }
    }
}