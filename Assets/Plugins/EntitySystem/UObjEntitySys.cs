using System;
using System.Collections.Generic;
using GameBase.Tools;

namespace GameBase.EntitySystem
{
    public abstract class UObjEntitySys<T_Entity, T_UObject, T_Instance> : CommonEntitySys<T_Entity, T_Instance>
        where T_Entity : class, IUEntity<T_UObject>, new()
        where T_UObject : new()
        where T_Instance : UObjEntitySys<T_Entity, T_UObject, T_Instance>, new()
    {
        protected Dictionary<int, EUObjectPool<T_Entity, T_UObject>> _objPools = new();

        protected abstract T_UObject InstantiateObj(T_Entity e);

        /// <summary>
        /// 从对象池中获取UObject时调用
        /// </summary>
        /// <param name="e"></param>
        protected virtual void AfterInstantiateEUObject(T_Entity e) { }

        /// <summary>
        /// 释放UObject回对象池时调用
        /// </summary>
        /// <param name="e"></param>
        protected virtual void BeforeReleaseEUObject(T_Entity e) { }

        protected sealed override void OnRemoveEntityFromActives(T_Entity e)
        {
            BeforeReleaseEUObject(e);

            if (e.ObjID < 0)
            {
                Tools.XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"entity:{e}-->id out of defined, id:{e.ObjID}");
                return;
            }
            
            _objPools[e.ObjID].Release(e.Obj);
        }

        protected sealed override void OnRegisterEntityToActives(T_Entity e)
        {
            if (e.ObjID < 0)
            {
                Tools.XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"entity:{e}-->id out of defined, id:{e.ObjID}");
                return;
            }

            
            if (!_objPools.ContainsKey(e.ObjID))
            {
                _objPools[e.ObjID] = new EUObjectPool<T_Entity, T_UObject>();
                _objPools[e.ObjID].InstantiateFunc = InstantiateObj;
            }

            e.Obj = _objPools[e.ObjID].Get(e);

            AfterInstantiateEUObject(e);
        }
    }
}
