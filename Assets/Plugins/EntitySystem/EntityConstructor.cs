using GameBase.Tools;
using NReco.Csv;
using System;
using System.IO;
using UnityEngine;

namespace GameBase.EntitySystem
{
    public abstract class EntityConstructor<T_Data, T_Entity, T_Container, T_EntitySys, T_Constructor> : BaseConstructor<T_Data, T_Entity, T_Constructor>
        where T_Data : struct
        where T_Entity : class, IEntity, new()
        where T_Container : IEContainer, new()
        where T_Constructor : EntityConstructor<T_Data, T_Entity, T_Container, T_EntitySys, T_Constructor>, new()
        where T_EntitySys: SimplestEntitySys<T_Entity, T_Container,  T_EntitySys>, new()
    {
        protected abstract T_EntitySys SysInstance { get; }

        protected override T_Entity Get(Action<T_Entity> Init)
        {
            return SysInstance.NewEntity(Init);
        }
    }
}
