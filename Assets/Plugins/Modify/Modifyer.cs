using GameBase.Tools;
using System;

namespace GameBase.Modify
{
    public enum ModifyType
    {
        Aways,
        Periodoic,
    }

    public class Modifyer<T> : IEntity
    {
        public ModifyType type = ModifyType.Aways;
        public bool trigOnGive;

        public float duration;
        public float dt;
        public Func<T, T, T> ModifyFunc;
        public Func<bool> ClearTrigger;

        internal float instantiateTime;
        internal float lastEnableTime;
        internal bool enable;
        internal bool isRelease;
        int IEntity.InstanceID { get; set; }
    }
}
