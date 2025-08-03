using GameBase.Modify;
using UnityEngine;

namespace GameBase.Buff
{
    public class GBuff:
        IViewableBuff
    {
        protected MonoBehaviour _caster; // 施加者
        protected IModifyable _target; // 施加目标
        internal IModifyable Target => _target;

        float IViewableBuff.DurationRemain => durationRemain;

        float IViewableBuff.DurationSet => duration;

        int IViewableBuff.StackNum => stackNum;

        #region attribute
        public int maxStacks;  // 最大叠加层数
        public float duration; // 持续时间
        internal float durationRemain; // 剩余持续时间
        internal float castTime; // 施加时间
        public int stackNum;   // 当前叠加层数
        #endregion

        private bool _clearFlag = false;

        #region public method

        // 创建一个buff，buff必须要有创建者
        public GBuff(MonoBehaviour caster)
        {
            _caster = caster;
        }

        // 清除buff，由于buff既需要时间到自动清除，也可以被驱散手段清除，所以设置为public
        public void Clear()
        {
            if (_clearFlag) return;
            BuffMgr.Remove(this);
            _clearFlag = true;
            OnClear();
        }

        // 将buff施加给某个对象
        public void CastTo(IModifyable o)
        {
            _target = o;
            castTime = Time.time;
            durationRemain = duration;
            if (BuffMgr.HasBuff(o, this))
            {
                OnReCast();
            }
            else
            {
                OnCast();
                BuffMgr.Add(o, this);
            }
        }

        // buff是否被某个对象拥有
        public bool OwnBy(IModifyable o)
        {
            return BuffMgr.HasBuff(o, this);
        }
        #endregion

        #region virtual method
        // 当buff被重新施加时调用
        protected virtual void OnReCast()
        {

        }

        // buff被施加时调用
        protected virtual void OnCast()
        {

        }

        // buff被清除时调用
        protected virtual void OnClear()
        {

        }
        #endregion
    }
}
