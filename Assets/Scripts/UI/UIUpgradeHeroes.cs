using frameworks.services;
using frameworks.ui;
using System;
using System.Collections.Generic;
using towerdefence.configs;
using towerdefence.services;
using UnityEngine;

namespace towerdefence.ui
{
    public class UIUpgradeHeroes : UIScreen
    {
        [InjectService] private HerosRosterService mHeroRosterService;

        [SerializeField] private Transform _HeroUpgradeCardsHolder;
        [SerializeField] private UIHeroUpgradeCard _HeroUpgradeCardPrefab;
        [SerializeField] private Transform _CharacterPreviewsHolder;

        protected override void Awake()
        {
            base.Awake();
            InitializeUpgradeCards();
        }

        private void InitializeUpgradeCards()
        {
            float xOffset = 0f;

            List<HeroInfo> heroInfos = mHeroRosterService.GetHeroInfos();
            foreach (HeroInfo heroInfo in heroInfos)
            {
                UIHeroUpgradeCard upgradeCard = Instantiate(_HeroUpgradeCardPrefab, _HeroUpgradeCardsHolder);
                upgradeCard.InitializeCard(heroInfo);

                GameObject obj = Instantiate(heroInfo.CharacterPreviewPrefab, _CharacterPreviewsHolder);
                obj.transform.position = new Vector3(xOffset, 0f, 0f);
                xOffset -= 5f;
            }
        }
    }
}