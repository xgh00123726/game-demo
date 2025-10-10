using GameBase.Spells;

namespace Constructor.Spells.Action
{
    public abstract class BaseModifier
    {
        private ModifyableAction _action;

        public void ModifyTo(ISpellAction action, int index)
        {
            if (action is ModifyableAction mAct)
            {
                mAct.AddModifier(this, index);
                _action = mAct;
            }
        }

        public void ModifyTo(Spell spell, int index)
        {
            ModifyTo(spell.action, index);
        }

        internal abstract void Modify(ref ModifyableModifyData modifyData);
    }
}
