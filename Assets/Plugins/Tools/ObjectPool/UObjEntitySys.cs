using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    public abstract class UObjEntitySys<T_entity, T_entityContainer, T_UObject, T_UObjectContainer> : SimplestEntitySys<T_entity, T_entityContainer>
        where T_entity : IEntity<T_UObject>, new()
        where T_entityContainer : IEntityContainer<T_entity>, IEnumerable<T_entity>, new()
        where T_UObjectContainer : IEntityContainer<T_UObject>, IEnumerable<T_UObject>, new()
    {
        private static UObjEntitySys<T_entity, T_entityContainer, T_UObject, T_UObjectContainer> _instance;
        public new static UObjEntitySys<T_entity, T_entityContainer, T_UObject, T_UObjectContainer> Instance => _instance;

        protected T_UObjectContainer[] _objContainers;

        protected abstract int ContainerCapacity { get; }

        protected abstract T_UObject InstantiateObj(IEntity<T_UObject> e);
        protected abstract void OnInstantiateUObject(T_entity e);

        protected abstract void OnReleaseUObject(T_entity e);

        protected override void OnRemoveEntity(T_entity e)
        {
            if (e.ID >= ContainerCapacity || e.ID < 0)
            {
                Tools.XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"entity:{e}-->id out of defined, id:{e.ID}, max:{ContainerCapacity}");
                return;
            }

            OnReleaseUObject(e);
            _objContainers[e.ID].Release(e.Obj);
        }

        protected override void OnRegisterEntity(T_entity e)
        {
            if (e.ID >= ContainerCapacity || e.ID < 0)
            {
                Tools.XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"entity:{e}-->id out of defined, id:{e.ID}, max:{ContainerCapacity}");
                return;
            }

            var container = _objContainers[e.ID];
            if (container == null)
            {
                container = new T_UObjectContainer();
            }
            if (container.Empty)
            {
                e.Obj = InstantiateObj(e);
                container.Add(e.Obj);
            }
            else
            {
                e.Obj = container.Get();
            }
            _objContainers[e.ID] = container;
            
            OnInstantiateUObject(e);
        }

        protected override void Awake()
        {
            base.Awake();

            _instance = this;
            _objContainers = new T_UObjectContainer[ContainerCapacity];
        }
    }
}
