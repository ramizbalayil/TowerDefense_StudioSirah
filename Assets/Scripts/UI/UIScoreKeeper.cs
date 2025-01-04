using frameworks.ioc;
using frameworks.services;
using frameworks.services.events;
using TMPro;
using towerdefence.events;
using towerdefence.services;
using UnityEngine;

namespace towerdefence.ui
{
    public class UIScoreKeeper : BaseBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ScoreLabel;

        [InjectService] private EventHandlerService mEventHandlerService;
        [InjectService] private ScoreKeeperService mScoreKeeperService;

        protected override void Awake()
        {
            base.Awake();
            mEventHandlerService.AddListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
            mScoreKeeperService.ResetScore();
            UpdateScoreLabel();
        }

        private void UpdateScoreLabel()
        {
            _ScoreLabel.text = "Score : " + mScoreKeeperService.LevelScore;
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<EnemyReachedDestinationEvent>(OnEnemyReachedDestinationEvent);
        }

        private void OnEnemyReachedDestinationEvent(EnemyReachedDestinationEvent e)
        {
            mScoreKeeperService.ReduceScore();
            UpdateScoreLabel();
        }
    }
}