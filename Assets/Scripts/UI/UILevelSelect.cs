using frameworks.services;
using frameworks.services.events;
using frameworks.services.scenemanagement;
using frameworks.ui;
using System;
using towerdefence.configs;
using towerdefence.events;
using towerdefence.services;
using UnityEngine;

namespace towerdefence.ui
{
    public class UILevelSelect : UIScreen
    {
        [InjectService] SceneManagementService mSceneManagementService;
        [InjectService] LevelLoaderService mLevelLoaderService;
        [InjectService] private EventHandlerService mEventHandlerService;

        [SerializeField] private UILevelButton _LevelButtonPrefab;
        [SerializeField] private Transform _LevelButtonsHolder;

        protected override void Awake()
        {
            base.Awake();
            InitializeLevelSelectGrid();
            mEventHandlerService.AddListener<LevelSelectedEvent>(OnLevelSelectedEvent);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<LevelSelectedEvent>(OnLevelSelectedEvent);
        }

        private void OnLevelSelectedEvent(LevelSelectedEvent e)
        {
            mLevelLoaderService.LoadLevelInfo(e.Level);
            mSceneManagementService.LoadSceneAsync(2);
        }

        private void InitializeLevelSelectGrid()
        {
            LevelInfo[] levelInfos = mLevelLoaderService.GetLevelInfos();
            for (int i = 0; i < levelInfos.Length; i++)
            {
                UILevelButton levelButton = Instantiate(_LevelButtonPrefab, _LevelButtonsHolder);
                levelButton.Initialise(i + 1, levelInfos[i].Locked);
            }
        }
    }
}