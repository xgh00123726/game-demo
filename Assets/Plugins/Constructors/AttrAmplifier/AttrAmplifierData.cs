using GameBase.EntitySystem;
using GameBase.Items;
using System.Collections.Generic;

namespace Constructor.AttrAmplify
{
    public class AttrAmplifierData : ItemData, INamedData
    {
        public string TextureName { get; set; }
        public int Rarity { get; set; }
        public string Key { get; set; }
        public float ValueMin {  set; get; }
        public float ValueMax { set; get; }
        public string Name { get; set; }

        public int IntKey { get; set; }
    }
}
