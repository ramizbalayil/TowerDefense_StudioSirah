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


        public void UpdateUIHealth(float healthPerc)
        {
            healthPerc = Mathf.Max(0f, healthPerc);
            _HealthBar.fillAmount = healthPerc;
            UpdateUIHealthColor(healthPerc);
        }

        private void UpdateUIHealthColor(float healthPerc)
        {
            if (healthPerc > 0.5f)
                _HealthBar.color = _HealthColorNormal;
            else if (healthPerc > 0.25)
                _HealthBar.color = _HealthColorWarning;
            else
                _HealthBar.color = _HealthColorDanger;
        }
    }
}