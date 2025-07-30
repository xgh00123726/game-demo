using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    // 管理类型T的对象，T必须是可对象池化的
    public class CSObjectPool<T> : UObjectPool<T>
        where T : IPoolableObject
    {
        protected override void OnInstantiate(T obj)
        {
            base.OnInstantiate(obj);
            obj.OnInstantiate();
        }

        protected override void OnRelease(T obj)
        {
            base.OnRelease(obj);
            obj.OnRelease();
        }
        public CSObjectPool()
        {

        }
    }

}