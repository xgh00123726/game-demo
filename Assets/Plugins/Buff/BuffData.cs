using System.Collections.Generic;

namespace GameBase.Buffs
{
    public struct ModifierPair
    {
        public string Key { get; set; }
        public float Value { get; set; }

        public ModifierPair(string key, float value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    public class BuffData
    {
        public string textureName;
        public int rarity;
        public List<KeyValuePair<int, float>> iModifiers;
        public BuffTag tagEnum;

        public string tag;
        public List<ModifierPair> modifiers;
    }
}
