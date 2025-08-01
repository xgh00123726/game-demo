using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyableAttrs
    {
        public ModifyableValue<float> coolingAcclerate = 0;
        public ModifyableValue<float> HPMax = 100;
        public ModifyableValue<float> damage = 0;
        public ModifyableValue<float> defense = 0;
        internal void Reset()
        {
            coolingAcclerate.Reset();
            HPMax.Reset();
            damage.Reset();
            defense.Reset();
        }
    }
}
