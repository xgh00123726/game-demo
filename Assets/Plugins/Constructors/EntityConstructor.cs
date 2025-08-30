using GameBase.EntitySystem;
using System;

namespace Constructor
{
    public abstract class EntityConstructor<T_Data, T_Entity, T_EntitySys, T_Constructor> : BaseConstructor<T_Data, T_Entity, T_Constructor>
        where T_Data : struct
        where T_Entity : class, IEntity, new()
        where T_Constructor : EntityConstructor<T_Data, T_Entity, T_EntitySys, T_Constructor>, new()
        where T_EntitySys: CommonEntitySys<T_Entity,  T_EntitySys>, new()
    {
        protected abstract T_EntitySys SysInstance { get; }

        protected override T_Entity Get(Action<T_Entity> Init)
        {
            return SysInstance.NewEntity(Init);
        }
    }
}
