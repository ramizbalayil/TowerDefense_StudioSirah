using frameworks.ioc;
using frameworks.services.events;
using frameworks.services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using towerdefence.events;

namespace towerdefense.ui
{
    public class UIHeroSelector : BaseBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [InjectService] private EventHandlerService mEventHandlerService;

        [SerializeField] private TextMeshProUGUI _HeroLabel;
        [SerializeField] private RawImage _CharacterPreview;

        private string mHeroId;
        public void InitializeHeroSelector(string heroId, Texture characterPreviewRenderTexture)
        {
            mHeroId = heroId;
            _HeroLabel.text = mHeroId;
            _CharacterPreview.texture = characterPreviewRenderTexture;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            mEventHandlerService.TriggerEvent(new DragHeroSpawnPoint(mHeroId, true));
        }

        public void OnDrag(PointerEventData eventData)
        {
            mEventHandlerService.TriggerEvent(new DragHeroSpawnPoint(mHeroId, true));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            mEventHandlerService.TriggerEvent(new DragHeroSpawnPoint(mHeroId, false));
            gameObject.SetActive(false);
        }
    }
}
