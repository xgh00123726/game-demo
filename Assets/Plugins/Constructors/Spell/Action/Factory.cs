using Constructor.Spells.Action.Modifyables;
using GameBase.Spells;
using System;
using System.Collections.Generic;
using GameBase.EntitySystem;

namespace Constructor.Spells.Action
{
    public enum Type
    {
        TriggerOnHit,
        NearestTarget,
        AreaFixedDis,
        BuffSelf,

        MAreaFixedDis,
        MNearestTarget,
    }
    public class Factory : ConstructorFactory<Type, ISpellAction, Factory>
    {
        protected override Dictionary<Type, Func<int, ISpellAction>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.TriggerOnHit, TriggerOnHitCon.Instance.Get },
                {Type.NearestTarget, NearestTargetCon.Instance.Get },
                {Type.AreaFixedDis, AreaFixedDisCon.Instance.Get },
                {Type.BuffSelf, BuffSelfCon.Instance.Get },
                {Type.MAreaFixedDis, MAreaFixedDisCon.Instance.Get },
                {Type.MNearestTarget, MNearestTargetCon.Instance.Get },
            };
        }
    }
}
