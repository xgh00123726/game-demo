using UnityEngine;

namespace GameBase.Spell
{
    public partial class GSpell
    {
        public GSpell()
        {
            SpellMgr.Addspell(this);
        }

        public delegate bool SpellCondition(GSpell spell);
        public delegate void SpellAction(GSpell spell);
        public SpellCondition ChooseCondition;
        public SpellCondition CancelCondition;
        public SpellCondition InvokeCondition;

        public float _spellMoment;             // 施法时刻
        public float coolingTimeSet;           // 冷却时间
        public float _coolingTimeRemain;       // 剩余冷却时间
        public bool isTargetable = true;       // 技能是否为目标型技能
        private bool _hasTarget = false;       // 是否具有目标
        public ISpellTarget _target;           // 技能目标
        public SpellAction CastAction;         // 技能动作

        protected ISpeller _speller;
        protected bool _hasSpeller = false;


        public ISpellTarget Target
        {
            get => _target;
            set
            {
                if (value == null) return;

                _hasTarget = true;
                _target = value;
            }
        }
        public bool HasTarget => _hasTarget;
        public ISpeller Speller
        {
            get => _speller;
            set
            {
                _hasSpeller = true;
                _speller = value;
            }
        }

        protected virtual void OnChoosing()
        {
            if (_hasIndicator)
            {
                _indicator.Move();
            }
        }

        protected virtual void OnChoose()
        {
            if (_hasIndicator)
            {
                _indicator.Show();
            }
        }

        protected virtual void OnCancel()
        {
            if (_hasIndicator)
            {
                _indicator.Hide();
            }
        }

        protected virtual void OnCast()
        {
            if (_hasIndicator)
            {
                _indicator.Hide();
            }
        }
        private void SpellCast()
        {
            _spellMoment = Time.time;
            _coolingTimeRemain = coolingTimeSet;
            CastAction?.Invoke(this);
            OnCast();
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
