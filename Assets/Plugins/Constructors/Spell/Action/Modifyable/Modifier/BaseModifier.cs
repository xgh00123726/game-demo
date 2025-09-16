using GameBase.Spells;

namespace Constructor.Spells.Action.Modifyables.Modifier
{
    public abstract class BaseModifier
    {
        private ModifyableAction _action;

        public void ModifyTo(IAction action, int index)
        {
            if (action is ModifyableAction mAct)
            {
                mAct.AddModifier(this, index);
                _action = mAct;
            }
        }

        public void ModifyTo(Spell spell, int index)
        {
            ModifyTo(spell.actionInterface, index);
        }

        internal abstract void Modify(ref ModifyableModifyData modifyData);
    }
}
