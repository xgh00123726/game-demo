using Constructor.Spells.Action.Modifyables;
using GameBase.Spells;
using System;
using System.Collections.Generic;
using GameBase.EntitySystem;

namespace Constructor.Spells.Action
{
    public enum Type
    {
        MTriggerOnHit,
        MBuffSelf,

        MAreaFixedDis,
        MNearestTarget,
    }
    public class Factory : ConstructorFactory<Type, ISpellAction, Factory>
    {
        protected override Dictionary<Type, Func<int, ISpellAction>> GetConstructorGetDict()
        {
            return new()
            {
                {Type.MTriggerOnHit, MTriggerOnHitCon.Instance.Get },
                {Type.MBuffSelf, BuffSelfCon.Instance.Get },
                {Type.MAreaFixedDis, MAreaFixedDisCon.Instance.Get },
                {Type.MNearestTarget, MNearestTargetCon.Instance.Get },
            };
        }
    }
}
