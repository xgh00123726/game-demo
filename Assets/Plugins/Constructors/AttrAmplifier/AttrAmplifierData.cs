using GameBase.EntitySystem;
using GameBase.Items;
using System.Collections.Generic;

namespace Constructor.AttrAmplify
{
    public struct ModifierPair
    {
        public string Key { get; set; }
        public float ValueMin { get; set; }
        public float ValueMax { get; set; }

        public ModifierPair(string key, float valueMin, float valueMax)
        {
            Key = key;
            ValueMax = valueMax;
            ValueMin = valueMin;
        }
    }
    public class AttrAmplifierData : ItemData, INamedData
    {
        public string TextureName { get; set; }
        public int Rarity { get; set; }
        public int Key { get; set; }
        public List<KeyValuePair<int, float>> IntKeyModifier { get; set; }
        public List<ModifierPair> Modifier {  get; set; }
        public string Name { get; set; }
    }
}
