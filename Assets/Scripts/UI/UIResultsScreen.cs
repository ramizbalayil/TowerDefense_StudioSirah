using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using frameworks.services.scenemanagement;
using System;
using TMPro;
using towerdefence.events;
using UnityEngine;
using UnityEngine.UI;

namespace towerdefence.ui
{
    public class UIResultsScreen : BaseBehaviour
    {
        [InjectService] private EventHandlerService mEventHandlerService;
        [InjectService] private SceneManagementService mSceneManagementService;


        [SerializeField] private TextMeshProUGUI _ResultLabel;
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
            _ResultLabel.text = e.Won ? "You Won" : "You Lost";
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