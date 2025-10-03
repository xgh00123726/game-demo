using Constructor.Spells.Action.Modifyables;
using GameBase.Spells;
using System;
using System.Collections.Generic;
using GameBase.EntitySystem;

namespace Constructor.Spells.Action
{
    public enum Type
    {
        TrigOnRelease,
        TrigNearestTarget,
        AreaFixedDis,
        BuffSelf,

        MAreaFixedDis,
        MTrigNearestTarget,
    }
    public class Factory : ConstructorFactory<Type, ISpellAction, Factory>
    {
        protected override Dictionary<Type, Func<int, ISpellAction>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.TrigOnRelease, TrigOnReleaseCon.Instance.Get },
                {Type.TrigNearestTarget, TrigNearestTargetCon.Instance.Get },
                {Type.AreaFixedDis, AreaFixedDisCon.Instance.Get },
                {Type.BuffSelf, BuffSelfCon.Instance.Get },
                {Type.MAreaFixedDis, MAreaFixedDisCon.Instance.Get },
                {Type.MTrigNearestTarget, MTrigNearestTargetCon.Instance.Get },
            };
        }
    }
}
