using GameBase.EntitySystem;
using System;

namespace Constructor
{
    public abstract class EntityConstructor<T_Data, T_Entity, T_EntitySys, T_Constructor> : BaseConstructor<T_Data, T_Entity, T_Constructor>
        where T_Data : struct
        where T_Entity : class, new()
        where T_Constructor : EntityConstructor<T_Data, T_Entity, T_EntitySys, T_Constructor>, new()
        where T_EntitySys: CommonEntitySys<T_Entity,  T_EntitySys>, new()
    {
        protected abstract T_EntitySys SysInstance { get; }

        protected abstract void ESet(T_Entity e, in T_Data data);

        protected override T_Entity Get()
        {
            return SysInstance.NewFromPool();
        }

        protected sealed override void Set(T_Entity e, in T_Data data)
        {
            ESet(e, in data);
            SysInstance.RegisterEntity(e);
        }
    }
}
