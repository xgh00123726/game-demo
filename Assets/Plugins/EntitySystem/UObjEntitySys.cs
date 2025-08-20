using System;
using System.Collections.Generic;
using GameBase.Tools;

namespace GameBase.EntitySystem
{
    public abstract class UObjEntitySys<T_Entity, T_Container, T_UObject, T_Instance> : SimplestEntitySys<T_Entity, T_Container, T_Instance>
        where T_Entity : class, IUEntity<T_UObject>, new()
        where T_Container : IEContainer, new()
        where T_UObject : new()
        where T_Instance : UObjEntitySys<T_Entity, T_Container, T_UObject, T_Instance>, new()
    {
        protected Dictionary<int, EUObjectPool<T_Entity, T_UObject>> _objPools = new();
        protected Dictionary<T_Entity, Action> _afterInstantiateDelegates = new();

        protected abstract T_UObject InstantiateObj(T_Entity e);

        protected abstract void AfterInstantiateEUObject(T_Entity e);

        protected abstract void BeforeReleaseEUObject(T_Entity e);

        protected override void OnRemoveEntityFromActives(T_Entity e)
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

        protected override void OnRegisterEntityToActives(T_Entity e)
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

            if (_afterInstantiateDelegates.ContainsKey(e))
            {
                _afterInstantiateDelegates[e]?.Invoke();
                _afterInstantiateDelegates.Remove(e);
            }
            
            AfterInstantiateEUObject(e);
        }
    }
}
