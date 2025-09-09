using GameBase.Spells;

namespace Constructor.Spells.Action.Modifyables.Modifier
{
    public abstract class BaseModifier
    {
        public void ModifyTo(IAction action)
        {
            if (action is ModifyableAction mAct)
            {
                mAct.AddModifier(this);
            }
        }

        public void ModifyTo(Spell spell)
        {
            ModifyTo(spell.actionInterface);
        }
        internal abstract void Modify(ref ModifyableModifyData modifyData);
    }
}
