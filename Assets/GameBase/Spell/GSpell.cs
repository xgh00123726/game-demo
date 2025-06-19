using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spell
{
    public class GSpell
    {
        private ISpeller _speller;
        public GSpell(ISpeller speller)
        {
            this._speller = speller;
            SpellMgr.Addspell(this);
        }

        public delegate bool SpellCondition(GSpell spell);
        public SpellCondition ChooseCondition;
        public SpellCondition CancelCondition;
        public SpellCondition InvokeCondition;

        public float _spellMoment;             // 施法时刻
        public float coolingTimeSet;           // 冷却时间
        public float _coolingTimeRemain;       // 剩余冷却时间

        public float CoolingTimeRemain => _coolingTimeRemain > 0 ? _coolingTimeRemain : 0;
        public float CoolingTimeRemainPercent => CoolingTimeRemain / coolingTimeSet;
        public bool IsCoolDown => _coolingTimeRemain <= 0;
        private SequentialBool _isChoosing = (SequentialBool)false;
        public virtual bool IsChoosing => _isChoosing;

        #region virtual method
        protected virtual void OnChoosing()
        {
        }

        protected virtual void OnChoose()
        {

        }

        protected virtual void OnCancel()
        {
        }

        // 空函数，需完全重写
        protected virtual void OnCast()
        {
        }
        #endregion
        private void SpellCast()
        {
            _spellMoment = Time.time;
            _coolingTimeRemain = coolingTimeSet;
            OnCast();
        }

        private void CoolDownFixedUpdate()
        {
            if (_coolingTimeRemain > 0)
            {
                _coolingTimeRemain -= Time.fixedDeltaTime
                    * (_speller.CoolingAccelerate * 0.01f + 1);
            }
        }

        internal void Update()
        {
            bool isCoolDown = IsCoolDown;

            // 冷却完毕，且不在选择，如果触发了选择事件，则选择标志位置true
            if (isCoolDown && !IsChoosing && ChooseCondition?.Invoke(this) == true)
            {
                _isChoosing.Set();
            }
            // 冷却完毕，且正在选择，如果触发了取消事件，则选择标志位置false
            if (isCoolDown && IsChoosing && CancelCondition?.Invoke(this) == true)
            {
                _isChoosing.Reset();
            }
            // 冷却完毕，且正在选择，如果触发了施法事件，则触发施法事件回调
            if (isCoolDown && IsChoosing && InvokeCondition?.Invoke(this) == true)
            {
                SpellCast();
                _isChoosing.Reset();
            }

            // 正在选择时触发正在选择回调
            if (IsChoosing)
            {
                OnChoosing();
            }
            // 选择标志位上升沿触发选择回调
            if (_isChoosing.EdgeRising)
            {
                OnChoose();
            }
            // 选择标志位下降沿触发取消回调
            if (_isChoosing.EdgeFalling)
            {
                OnCancel();
            }
        }

        // 技能每帧更新逻辑
        internal void FixedUpdate()
        {
            CoolDownFixedUpdate(); // 跑技能冷却逻辑
        }
    }
}
