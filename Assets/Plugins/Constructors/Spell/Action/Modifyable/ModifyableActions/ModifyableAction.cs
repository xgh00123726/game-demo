using Constructor.Spells.Action.Modifyables.Modifier;
using GameBase.Spells;
using GameBase.Tools;
using System.Collections.Generic;

namespace Constructor.Spells.Action.Modifyables
{
    public abstract class ModifyableAction : IAction
    {
        private ModifyableModifyData _data;
        private ModifyableModifyData _modifiedData;
        private List<BaseModifier> _modifiers = new();

        public ModifyableAction()
        {
            _data.flyingDistance = 0;
            _data.flyingNums = 0;
            _data.fireDisfuse = 0;
            _data.castTimes = 0;
        }

        protected abstract void CastAction(Spell spell, in ModifyableModifyData modifyData);

        void IAction.CastAction(Spell spell)
        {
            CastAction(spell, in _modifiedData);
            if (_modifiedData.castTimes > 0)
            {
                for (int i = 1; i <= _modifiedData.castTimes; ++i)
                {
                    Timer.AddTask(i * 0.2f, () => CastAction(spell, in _modifiedData));
                }
            }
        }

        public void AddModifier(BaseModifier modifier)
        {
            _modifiers.Add(modifier);
            ResolveModifiedData();
        }

        public void RemoveModifyer(BaseModifier modifier)
        {
            _modifiers.Remove(modifier);
            ResolveModifiedData();
        }

        public void RemoveModifyer(int index)
        {
            _modifiers.RemoveAt(index);
            ResolveModifiedData();
        }

        public void ResolveModifiedData()
        {
            _modifiedData = _data;
            foreach (var modifier in _modifiers)
            {
                modifier.Modify(ref _modifiedData);
            }
        }
    }
}
