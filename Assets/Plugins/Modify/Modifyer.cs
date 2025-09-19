using GameBase.EntitySystem;
using GameBase.Tools;
using System;

namespace GameBase.Modify
{
    public enum ModifyType
    {
        Aways     = 1 << 0 ,    // 常驻生效，每个modify周期都会触发一次modify
        Periodoic = 1 << 1 ,    // 周期生效，当达到dt时，触发modify
        Once      = 1 << 2 ,    // 生效一次

        Temporary = 1 << 13,    // 属性永久变更，常用于掉血掉蓝，吃永久增益等
        Forever   = 1 << 14,    // 属性暂时变更，用于buff，装备等
    }

    public class Modifyer :
        IPoolable
    {
        public ModifyType type = ModifyType.Once | ModifyType.Forever;
        public bool trigOnGive;

        public float duration;
        public float dt;
        public float value;
        public bool externalClear;
        public Action OnModify;

        internal float instantiateTime;
        internal float lastEnableTime;
        internal bool enable;
        internal bool modifyableRelease;
        internal bool isRelease;

        void IPoolable.AfterGet()
        {
            isRelease = false;
        }

        void IPoolable.BeforeRelease()
        {
            OnModify = null;
        }
    }
}
