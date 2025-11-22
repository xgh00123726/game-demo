using System.Collections.Generic;
using GameBase.EntitySystem;
using GameBase.Items;

namespace GameBase.Buffs
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

    public class BuffData : ItemData, INamedData
    {
        public string Name { get; set; }
        public string TextureName { get; set; }
        public int Rarity { get; set; }
        public List<KeyValuePair<int, float>> IntKeyModifiers { get; set; }
        public BuffTag TagEnum { get; set; }

        public string Tag {  get; set; }
        public List<ModifierPair> Modifiers { get; set; }
    }
}
