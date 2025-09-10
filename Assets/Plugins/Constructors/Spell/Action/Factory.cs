using Constructor.Spells.Action.Modifyables;
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
        BuffSelf,

        MAreaFixedDis,
    }
    public class Factory : ConstructorFactory<Type, IAction, Factory>
    {
        protected override Dictionary<Type, Func<int, IAction>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.TrigOnRelease, TrigOnReleaseCon.Instance.Get },
                {Type.TrigNearestTarget, TrigNearestTargetCon.Instance.Get },
                {Type.AreaFixedDis, AreaFixedDisCon.Instance.Get },
                {Type.BuffSelf, BuffSelfCon.Instance.Get },
                {Type.MAreaFixedDis, MAreaFixedDisCon.Instance.Get },
            };
        }
    }
}
