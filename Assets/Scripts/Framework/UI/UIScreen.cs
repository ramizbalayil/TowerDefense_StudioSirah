using frameworks.ioc;
using UnityEngine;

namespace frameworks.ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIScreen : BaseBehaviour
    {
        public string ScreenID;

        private CanvasGroup mCanvasGroup;

        public void Init()
        {
            mCanvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show()
        {
            mCanvasGroup.alpha = 1f;
            mCanvasGroup.blocksRaycasts = true;
        }

        public virtual void Hide()
        {
            mCanvasGroup.alpha = 0f;
            mCanvasGroup.blocksRaycasts = false;
        }
    }
}