using GameBase.Tools;
using UnityEngine;
using GameBase.UI;
namespace GameBase.Health
{
    /// <summary>
    /// 管理血条预制件
    /// </summary>
    public class HealthBar : MonoBehaviour,
        IPoolableObject
    {
        internal IHealthBarOwner _owner;

        private void Awake()
        {
            //UIMgr.AttachToRoot(transform);
        }

        internal void _Update()
        {
            transform.position = _owner.HealthBarPosition;
        }

        void IPoolableObject.OnInstantiate()
        {
            gameObject.SetActive(true);
        }

        void IPoolableObject.OnRelease()
        {
            gameObject.SetActive(false);
        }
    }
}
