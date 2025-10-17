using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public class BaseObjectPool<T>
    {
        protected LinkedList<T> _objects = new LinkedList<T>();

        public Action<T> InstantiateAction;
        public Action<T> ReleaseAction;
        public Func<T> InstantiateFunc;

        // 对象池是否为空
        public bool Empty => _objects.Count == 0;

        public int Count => _objects.Count;

        // 向对象池中获取一个对象
        public virtual T Get()
        {
            T ret;
            if (Empty)
            {
                if (InstantiateFunc == null)
                {
                    XLogger.Instance.Level(XLogger.LogLevel.Error)
                        .Log($"trying to get object:{typeof(T).Name} from a empty pool");
                    return default;
                }
                else
                {
                    ret = InstantiateFunc();
                }
            }
            else
            {
                ret = _objects.First.Value;
                _objects.RemoveFirst();
            }

            if (ret is IPoolable iret)
            {
                iret.AfterGet();
            }

            InstantiateAction?.Invoke(ret);

            return ret;
        }

        /// <summary>
        /// 将一个对象强制放回对象池
        /// <list type="bullet">
        /// <item><param name="obj">需要被放回的对象</param></item>
        /// </list></summary>
        public virtual void Release(T obj)
        {
            if (obj is IPoolable iobj)
            {
                iobj.BeforeRelease();
            }

            ReleaseAction?.Invoke(obj);
            _objects.AddLast(obj);
        }
    }
}
