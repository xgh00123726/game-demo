using GameBase.Tools;
using System;

namespace GameBase.Modify
{
    public enum ModifyType
    {
        Aways     = 1 << 0 ,
        Periodoic = 1 << 1 ,
        Once      = 1 << 2 ,

        Temporary = 1 << 13,
        Forever   = 1 << 14,
    }

    public class Modifyer<T> : IEntity
    {
        public ModifyType type = ModifyType.Aways | ModifyType.Temporary;
        public bool trigOnGive;

        public float duration;
        public float dt;
        public Func<T, T, T> ModifyFunc;
        public Func<bool> ClearTrigger;

        internal float instantiateTime;
        internal float lastEnableTime;
        internal bool enable;
        internal bool modifyableRelease;
        internal bool isRelease;
        int IEntity.InstanceID { get; set; }
    }
}
