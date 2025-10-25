using GameBase.EntitySystem;
using GameBase.Tools;
using System;

namespace GameBase.Modify
{
    public enum ModifyType
    {
        Always    = 1 << 0 ,    // 常驻生效，每个modify周期都会触发一次modify
        Periodoic = 1 << 1 ,    // 周期生效，当达到dt时，触发modify
        Once      = 1 << 2 ,    // 生效一次

        Temporary = 1 << 13,    // 属性暂时变更，用于buff，装备等
        Forever   = 1 << 14,    // 属性永久变更，常用于掉血掉蓝，吃永久增益等
    }

    public class Modifyer
    {
        public ModifyType type = ModifyType.Once | ModifyType.Forever;
        public bool trigOnGive;

        public float dt;
        public float value;
        public Action OnModify;
        public Modifyable target;

        internal float instantiateTime;
        internal float lastEnableTime;

        public void AddTo(Modifyable target)
        {
            this.target = target;
        }

        public void Release()
        {
            target = null;
            ModifyerSys.Instance.RemoveEntity(this);
        }
    }
}
