using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.UI
{
    public class BasePanel : BaseUI
    {
        protected GameObject _UIComponents;
        protected virtual void Awake()
        {
            _UIComponents = transform.Find("UIComponents").gameObject;
            if (_UIComponents == null)
            {
                Debug.LogWarning("A panel must has a UIComponents");
            }
            Assert.IsNotNull(_UIComponents);
        }
    }
}
