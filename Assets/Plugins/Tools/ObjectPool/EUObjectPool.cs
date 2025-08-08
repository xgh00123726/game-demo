using System;
using System.Collections;
using System.Collections.Generic;

namespace GameBase.Tools
{
    /// <summary>
    /// 对象池，T_Entity为索引，T_UObject为保存的对象
    /// </summary>
    /// <typeparam name="T_Entity"></typeparam>
    /// <typeparam name="T_UObject"></typeparam>
    public class EUObjectPool<T_Entity, T_UObject> : BaseObjectPool<T_UObject>
        where T_Entity : IUEntity<T_UObject>, new()
    {
        public new Action<T_Entity> InstantiateAction;
        public new Action<T_Entity> ReleaseAction;
        public new Func<T_Entity, T_UObject> InstantiateFunc;

        // 向对象池中获取一个对象
        public T_UObject Get(T_Entity e)
        {
            T_UObject ret;
            if (Empty)
            {
                if (InstantiateFunc == null)
                {
                    XLogger.Instance.Level(XLogger.LogLevel.Error)
                        .Log("trying to get object from a empty pool");
                    return default;
                }
                else
                {
                    ret = InstantiateFunc(e);
                }
            }
            else
            {
                ret = _objects.First.Value;
                _objects.RemoveFirst();
            }

            InstantiateAction?.Invoke(e);
            
            return ret;
        }
    }
}
