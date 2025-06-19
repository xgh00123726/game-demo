using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyableAttrs
    {
        public ModifyableValue<float> coolingAcclerate = new() { Set = 0 };
        internal void Reset()
        {
            coolingAcclerate.Reset();
        }
    }
}
