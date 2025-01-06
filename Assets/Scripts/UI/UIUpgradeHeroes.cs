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

        protected override void Awake()
        {
            base.Awake();
            InitializeUpgradeCards();
        }

        private void InitializeUpgradeCards()
        {
            List<HeroInfo> heroInfos = mHeroRosterService.GetHeroInfos();
            foreach (HeroInfo heroInfo in heroInfos)
            {
                UIHeroUpgradeCard upgradeCard = Instantiate(_HeroUpgradeCardPrefab, _HeroUpgradeCardsHolder);
                upgradeCard.InitializeCard(heroInfo);
            }
        }
    }
}