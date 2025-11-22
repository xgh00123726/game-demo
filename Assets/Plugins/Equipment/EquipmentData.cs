using GameBase.EntitySystem;
using GameBase.Items;
using System.Collections.Generic;

namespace GameBase.Equipments
{
    public struct ModifierPair
    {
        public string Key { get; set; }
        public float Value { get; set; }

        public ModifierPair(string key, float value)
        {
            Key = key;
            Value = value;
        }
    }
    public struct IModifierPair
    {
        public int Key { get; set; }
        public float Value { get; set; }
    }
    public class EquipmentData : ItemData, INamedData
    {
        public string Name { get; set; }
        public string TextureName { get; set; }
        public int Rarity { get; set; }
        public List<IModifierPair> IntKeyModifiers { get; set; }
        public EquipmentTag TagEnum { get; set; }

        public string Tag {  get; set; }
        public List<ModifierPair> Modifiers { get; set; }
    }
}
