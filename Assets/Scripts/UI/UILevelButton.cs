using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using frameworks.ioc;
using UnityEngine.EventSystems;
using frameworks.services;
using frameworks.services.events;
using towerdefence.events;

namespace towerdefence.ui
{
    public class UILevelButton : BaseBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _Background;
        [SerializeField] private TextMeshProUGUI _LevelNumber;
        [SerializeField] private TextMeshProUGUI _LockedLabel;
        [SerializeField] private Color _LockedColor;
        [SerializeField] private Color _UnlockedColor;

        [InjectService] private EventHandlerService mEventHandlerService;

        private int mLevelNumber = 0;
        private bool mLocked;

        public void Initialise(int level, bool locked)
        {
            mLevelNumber = level;
            _LevelNumber.text = mLevelNumber.ToString();
            mLocked = locked;

            SetupLevelButton();
        }

        private void SetupLevelButton()
        {
            if (mLocked)
            {
                _Background.color = _LockedColor;
                _LockedLabel.text = "[LOCKED]";
            }
            else
            {
                _Background.color = _UnlockedColor;
                _LockedLabel.text = "";
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!mLocked)
                mEventHandlerService.TriggerEvent(new LevelSelectedEvent(mLevelNumber));
        }
    }
}