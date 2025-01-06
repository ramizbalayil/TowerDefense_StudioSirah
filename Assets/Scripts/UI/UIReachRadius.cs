using UnityEngine;

namespace towerdefence.ui
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class UIReachRadius : MonoBehaviour
    {
        public void SetSize(float size)
        {
            transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, 1f);
        }
    }
}
