using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Tools;

namespace GameBase.Resources
{
    public abstract class PoolablePrefab : MonoBehaviour, IPoolableObject
    {
        protected virtual void OnInstantiate()
        {
            gameObject.SetActive(true);
        }
        protected virtual void OnRelease()
        {
            gameObject.SetActive(false);
        }

        public virtual bool ReleaseTrigger => false;

        void IPoolableObject.OnInstantiate()
        {
            OnInstantiate();
        }

        void IPoolableObject.OnRelease()
        {
            OnRelease();
        }
    }
}
