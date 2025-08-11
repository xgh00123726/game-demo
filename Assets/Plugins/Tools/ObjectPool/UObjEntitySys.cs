using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
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

        protected virtual void BeforeInstantiateEUObject(T_Entity e) { }

        protected abstract void AfterInstantiateEUObject(T_Entity e);

        protected abstract void BeforeReleaseEUObject(T_Entity e);

        protected virtual void AfterReleaseEUObject(T_Entity e) { }

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
            AfterReleaseEUObject(e);
        }

        protected override void OnRegisterEntityToActives(T_Entity e)
        {
            BeforeInstantiateEUObject(e);

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

        /// <summary>
        /// 立刻创建一个对象，可以在对象被遍历时使用，会在下一帧将对象的unity对象创建出来
        /// </summary>
        /// <typeparam name="T_EntityType"></typeparam>
        /// <returns></returns>
        public T_EntityType NewEntity<T_EntityType>(Action AfterInstantiateUObjectDelegate) where T_EntityType : class, T_Entity, new()
        {
            ++entityNewTimes;

            _entityContainer.RegisterType<T_EntityType>();

            var e = _entityContainer.GetEntity<T_EntityType>();
            e.InstanceID = PoolInfo.allocatedID++;
            Register(e);
            _afterInstantiateDelegates[e] = AfterInstantiateUObjectDelegate;
            return e;
        }
    }
}
