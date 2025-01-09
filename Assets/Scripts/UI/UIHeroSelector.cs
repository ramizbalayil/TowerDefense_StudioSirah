using frameworks.ioc;
using frameworks.services.events;
using frameworks.services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using towerdefence.events;
using System;

namespace towerdefense.ui
{
    public class UIHeroSelector : BaseBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [InjectService] private EventHandlerService mEventHandlerService;

        [SerializeField] private TextMeshProUGUI _HeroLabel;
        [SerializeField] private RawImage _CharacterPreview;
        [SerializeField] private CanvasGroup _CanvasGroup;

        private string mHeroId;
        public void InitializeHeroSelector(string heroId, Texture characterPreviewRenderTexture)
        {
            mHeroId = heroId;
            _HeroLabel.text = mHeroId;
            _CharacterPreview.texture = characterPreviewRenderTexture;
            mEventHandlerService.AddListener<DragHeroSpawnPoint>(OnDragHeroSpawnPoint);
        }

        private void OnDestroy()
        {
            mEventHandlerService.RemoveListener<DragHeroSpawnPoint>(OnDragHeroSpawnPoint);
        }

        private void OnDragHeroSpawnPoint(DragHeroSpawnPoint e)
        {
            if (e.HeroId != mHeroId)
            {
                _CanvasGroup.alpha = e.IsDragging ? 0 : 1;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            mEventHandlerService.TriggerEvent(new DragHeroSpawnPoint(mHeroId, true));
            _CanvasGroup.alpha = 0.5f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            mEventHandlerService.TriggerEvent(new DragHeroSpawnPoint(mHeroId, true));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            mEventHandlerService.TriggerEvent(new DragHeroSpawnPoint(mHeroId, false));
            _CanvasGroup.alpha = 1f;
        }
    }
}
