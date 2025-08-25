using GameBase.Spells;
using System;
using System.Collections.Generic;

namespace Constructor.Spells.Action
{
    public enum Type
    {
        TrigOnRelease,
        TrigNearestTarget,
        AreaFixedDis,
        BuffSelf
    }
    public class Factory : ConstructorFactory<Type, IAction, Factory>
    {
        protected override Dictionary<Type, Func<int, IAction>> ConstructorGetDict =>
            new()
            {
                {Type.TrigOnRelease, TrigOnRelease.Instance.Get },
                {Type.TrigNearestTarget, TrigNearestTarget.Instance.Get },
                {Type.AreaFixedDis, AreaFixedDis.Instance.Get },
                {Type.BuffSelf, BuffSelf.Instance.Get },
            };
    }
}
