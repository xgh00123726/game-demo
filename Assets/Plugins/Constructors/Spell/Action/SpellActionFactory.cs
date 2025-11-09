using GameBase.Spells;
using GameBase.EntitySystem;
using Constructor.Spells.Action;

namespace Constructor.Spells
{
    public enum SpellActionType
    {
        MTriggerOnHit,
        MBuffSelf,

        MAreaFixedDis,
        MNearestTarget,
        MTriggerOnly,
    }
    public class SpellActionFactory : YamlFactory<SpellActionData, ISpellAction, SpellActionFactory>
    {
        protected override string YamlFolder => null;

        protected override ISpellAction GetEntity(SpellActionData data)
        {
            if (data == null) return null;

            if (data.type == SpellActionType.MTriggerOnHit)
            {
                return new MTriggerOnHit()
                {
                    data = data,
                    Size = data.slotNum,
                };
            }
            else if (data.type == SpellActionType.MBuffSelf)
            {
                return new MBuffSelf()
                {
                    data = data,
                    Size = data.slotNum,
                };
            }
            else if (data.type == SpellActionType.MAreaFixedDis)
            {
                return new MAreaFixedDis()
                {
                    data = data,
                    Size = data.slotNum,
                };
            }
            else if (data.type == SpellActionType.MNearestTarget)
            {
                return new MNearestTarget()
                {
                    data = data,
                    Size = data.slotNum,
                };
            }
            else if (data.type == SpellActionType.MTriggerOnly)
            {
                return new MTriggerOnly()
                {
                    data = data,
                    Size = data.slotNum,
                };
            }

            return null;
        }
    }
}
