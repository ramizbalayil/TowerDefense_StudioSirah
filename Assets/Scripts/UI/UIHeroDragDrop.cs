using frameworks.services;
using frameworks.services.events;
using frameworks.ui;
using System;
using System.Collections.Generic;
using towerdefence.configs;
using towerdefence.events;
using towerdefence.services;
using UnityEngine;
using UnityEngine.UI;

namespace towerdefense.ui
{
    public class UIHeroDragDrop : UIScreen
    {
        [InjectService] private HerosRosterService mHeroRosterService;
        [InjectService] private EventHandlerService mEventHandlerService;

        [SerializeField] private UIHeroSelector _UIHeroSelectorPrefab;
        [SerializeField] private Transform _HeroSelectorsContainer;
        [SerializeField] private Transform _CharacterPreviewsHolder;
        [SerializeField] private Button _StartGameButton;
        [SerializeField] private GameObject _BackgroundInfo;

        protected override void Awake()
        {
            base.Awake();
            InitializeHeroSelectors();
            mEventHandlerService.AddListener<DragHeroSpawnPoint>(OnDragHeroSpawnPoint);
            _StartGameButton.onClick.AddListener(OnStartGameButtonClicked);
        }

        private void OnDestroy()
        {
            _StartGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
            mEventHandlerService.RemoveListener<DragHeroSpawnPoint>(OnDragHeroSpawnPoint);
        }

        private void OnDragHeroSpawnPoint(DragHeroSpawnPoint e)
        {
            _BackgroundInfo.gameObject.SetActive(!e.IsDragging);
        }

        private void OnStartGameButtonClicked()
        {
            mEventHandlerService.TriggerEvent(new StartGameEvent());
            Hide();
        }

        private void InitializeHeroSelectors()
        {
            float xOffset = 0f;

            List<HeroInfo> heroInfos = mHeroRosterService.GetHeroInfos();
            foreach (HeroInfo heroInfo in heroInfos)
            {
                UIHeroSelector heroSelector = Instantiate(_UIHeroSelectorPrefab, _HeroSelectorsContainer);
                heroSelector.InitializeHeroSelector(heroInfo.HeroID, heroInfo.CharacterPreviewRenderTexture);

                GameObject obj = Instantiate(heroInfo.CharacterPreviewPrefab, _CharacterPreviewsHolder);
                obj.transform.position = new Vector3(xOffset, 0f, 0f);
                xOffset -= 5f;
            }
        }
    }
}