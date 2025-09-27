using Constructor.Spells.Action.Modifyables.Modifier;
using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.Tools;

namespace Constructor.Spells.Action.Modifyables
{
    public partial class ModifyableAction
    {
        public struct ModifierStore
        {
            public BaseModifier obj;
            public ModifierData data;
        }
        private ModifyableModifyData _data;
        private ModifyableModifyData _modifiedData;
        private DynInventory<ModifierStore> _modifiers = new();

        public int Size
        {
            get => _modifiers.Size;
            set => _modifiers.Size = value;
        }

        public void AddModifier(int inventoryID, int index)
        {
            var info = Modifier.Factory.Instance.GetInfo(inventoryID);
            var obj = Modifier.Factory.Instance.Get(info.type, info.typeID);
            _modifiers.Add(new ModifierStore()
            {
                obj = obj,
                data = new ModifierData()
                {
                    inventoryID = info.id,
                    type = info.type,
                    modifierID = info.typeID,
                }
            });
            ResolveModifiedData();
        }

        public bool HasItem(int index)
        {
            return _modifiers.HasItem(index);
        }

        public ModifierData GetData(int index)
        {
            return _modifiers[index].data;
        }

        public void AddModifier(BaseModifier modifier, int index)
        {
            _modifiers.Add(new ModifierStore()
            {
                obj = modifier,
            }, index);
            ResolveModifiedData();
        }

        public void RemoveModifyer(int index)
        {
            if (index < 0 || index >= _modifiers.Size)
            {
                XLogger.Instance.Log($"invalid index:{index}, max:{_modifiers.Size}");
                return;
            }
            _modifiers[index] = default;
            ResolveModifiedData();
        }

        public static void TryRemoveModifyer(Spell spell, int index)
        {
            if (spell.action is ModifyableAction mAct)
            {
                mAct.RemoveModifyer(index);
            }
        }

        protected void ResolveModifiedData()
        {
            _modifiedData = _data;
            for (int i = 0; i < _modifiers.Size; i++)
            {
                if (_modifiers[i].obj == null)
                {
                    continue;
                }

                _modifiers[i].obj.Modify(ref _modifiedData);
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
    }

}
