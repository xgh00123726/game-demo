using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.Tools;

namespace Constructor.Spells.Action
{
    public partial class ModifyableAction
    {
        private SpellActionModifierData _modifiedData;
        private DynInventory<int> _modifiers = new();

        public int Size
        {
            get => _modifiers.Size;
            set => _modifiers.Size = value;
        }

        public int GetID(int index)
        {
            if (_modifiers.HasItem(index))
            {
                return _modifiers[index];
            }

            return -1;
        }

        public void Swap(int p1, int p2)
        {
            _modifiers.Swap(p1, p2);
        }

        public void AddModifier(int id, int index)
        {
            _modifiers.Add(id, index);
            ResolveModifiedData();
        }

        public void RemoveModifier(int index)
        {
            if (index < 0 || index >= _modifiers.Size)
            {
                XLogger.Instance.Log($"invalid index:{index}, max:{_modifiers.Size}");
                return;
            }
            _modifiers.Remove(index);
            ResolveModifiedData();
        }

        public bool HasItem(int index)
        {
            return _modifiers.HasItem(index);
        }

        protected void ResolveModifiedData()
        {
            _modifiedData = default;
            for (int i = 0; i < _modifiers.Size; i++)
            {
                int id = GetID(i);
                if (id == -1)
                {
                    continue;
                }
                _modifiedData += SpellActionModifierDataBase.Instance[id];
            }

            if (_modifiedData.flyingNums < 0)
            {
                _modifiedData.flyingNums = 0;
            }

            _processedDisfuse = ProcessDisfuse(_modifiedData.fireDisfuse);
            if (_modifiedData.flyingNums > 0)
            {
                _angleInit = -_processedDisfuse / 2;
                _angleDelta = _processedDisfuse / _modifiedData.flyingNums;
            }
        }

        public static bool IsModifyable(Spell spell)
        {
            return spell.action is ModifyableAction;
        }
    }

}
