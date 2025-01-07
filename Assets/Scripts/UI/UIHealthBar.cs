using UnityEngine;
using UnityEngine.UI;

namespace towerdefence.ui
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _HealthBar;
        [SerializeField] private Color _HealthColorNormal;
        [SerializeField] private Color _HealthColorWarning;
        [SerializeField] private Color _HealthColorDanger;

        private float mStartFillAmount = 1f;
        private float mEndFillAmount = 1f;
        private float mElapsedTime = 0f;
        private float mDesiredDuration = 0.5f;
        public void UpdateUIHealth(float healthPerc)
        {
            healthPerc = Mathf.Max(0f, healthPerc);
            mStartFillAmount = _HealthBar.fillAmount;
            mEndFillAmount = healthPerc;
            mElapsedTime = 0f;
        }

        private void UpdateUIHealthColor()
        {
            float healthPerc = _HealthBar.fillAmount;
            if (healthPerc > 0.5f)
                _HealthBar.color = _HealthColorNormal;
            else if (healthPerc > 0.25)
                _HealthBar.color = _HealthColorWarning;
            else
                _HealthBar.color = _HealthColorDanger;
        }

        private void Update()
        {
            mElapsedTime += Time.deltaTime;
            _HealthBar.fillAmount = Mathf.Lerp(mStartFillAmount, mEndFillAmount, mElapsedTime / mDesiredDuration);
            UpdateUIHealthColor();
        }
    }
}