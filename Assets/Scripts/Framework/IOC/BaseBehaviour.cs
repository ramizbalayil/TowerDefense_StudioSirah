using UnityEngine;

namespace frameworks.ioc
{
    public class BaseBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            InjectBindings.Inject(this);
        }
    }
}