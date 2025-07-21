using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Object;
using GameBase.Modify;
using GameBase.Spell;
using GameBase.UI;
using GameBase.Projectile;
using GameBase.Resources;
using GameBase.Tools;
using Logger = GameBase.Tools.Logger;
using static GameBase.Entity.EntityMgr;

namespace GameBase.Entity
{
    public class GameEntity : BaseEntity,
        IModifyable,
        ISpeller,
        IProjectileTarget,
        IProjectileOwner,
        IHealthBarOwner,
        IPoolableObject
    {
        public enum DeadState
        {
            Default,
            HPZero,
            Dead,
        }

        public enum Camp
        {
            Neutral,
            Friendly,
            Rival
        }

        public ModifyableAttrs attrs = new ModifyableAttrs();  // 实体属性      如血量上限，移速，攻击力等
        public EntityInfo infos = new EntityInfo();            // 实体状态信息  如金钱，经验，当前血量
        
        
        public MoveComponent moveComponent;           // 移动组件
        public RotateComponent rotateComponent;       // 旋转组件
        public AnimationComponent animationComponent; // 动画组件
        public SphereCollider sphereCollider;         // 碰撞体

        #region register
        public HealthBar healthBar;                   // 血条

        #endregion

        protected string _prefabName = null;
        internal string PrefabName
        {
            get => _prefabName;
            set => _prefabName = value;
        }

        public List<GSpell> spells = new List<GSpell>();
        public Dictionary<GSpell, SpellItem> spellUI = new Dictionary<GSpell, SpellItem>();

        public Vector3 handOffset = new Vector3(0, 1, 0);      // 手部偏移，纠正射弹射出位置
        public Vector3 bodyOffset = new Vector3(0, 1, 0);      // 身体偏移，纠正被射弹击中位置
        public Vector3 healthBarOffset = new Vector3(0, 3, 0); // 血条偏移，使血条放在人物头部

        public DeadState _deadState = DeadState.Default;       // 死亡状态
        public Camp camp = Camp.Neutral;                       // 阵营
        private bool _isRegistered = false;                    // 是否注册
        public bool Register
        {
            get => _isRegistered;
            set
            {
                if (value && !_isRegistered)
                {
                    _isRegistered = true;                 // 置位true，防止重复初始化
                    _deadState = DeadState.Default;       // 将死亡状态置位true，确保游戏生命可以正常流动
                    infos.HP = attrs.HPMax;               // 死亡后重置血量
                    healthBar = HealthBarMgr.Get(this);   // 重新获取一个血条
                    EntityMgr.RegisterEntity(this);       // 将entity注册，便于全局管理
                    gameObject.SetActive(true);           // 将物体设置为可见
                    if (_prefabName == null)              // 设置预制件名字，便于对象池回收
                    {
                        _prefabName = GetType().Name;
                    }
                }
                else if (!value && _isRegistered)
                {
                    _isRegistered = false;
                    // 临时解决策略：防止场景结束时，场景自动释放掉healthBar，而gameobject destroy时又释放一遍
                    if (healthBar != null)
                    {
                        HealthBarMgr.Release(healthBar);
                    }
                    EntityMgr.UnRegisterEntity(this);
                    gameObject.SetActive(false);
                }
            }
        }
        ModifyableAttrs IModifyable.attrs => attrs;
        float ISpeller.CoolingAccelerate => attrs.coolingAcclerate.Value;

        Transform ISpeller.Transform => transform;

        Vector3 IProjectileTarget.Center => transform.position + bodyOffset;

        float IProjectileTarget.Radius => sphereCollider.radius;

        Vector3 IProjectileOwner.HandPostion => transform.position + handOffset;

        Vector3 IHealthBarOwner.HealthBarPosition => transform.position + healthBarOffset;

        void IPoolableObject.OnInstantiate()
        {
            Register = true;
        }

        void IPoolableObject.OnRelease()
        {
            Register = false;
        }

        public void AddSpell(GSpell spell, bool needUI = true)
        {
            spells.Add(spell);
            if (needUI)
            {
                spellUI[spell] = SpellPanel.Instance.AddItem();
            }
        }

        /// <summary>
        /// 受到伤害
        /// <list type="bullet">
        /// <item><param name="damageValue"><paramref name="damageValue"/>:伤害值</param></item>
        /// </list></summary>
        public virtual void GetDamage(float damageValue)
        {
            infos.HP -= Mathf.Max(0, damageValue - attrs.defense.Value);
            TextMgr.ShowDamageText(transform.position, damageValue);
            if (infos.HP <= 0)
            {
                if (_deadState == DeadState.Default)
                {
                    _deadState = DeadState.HPZero;
                    OnHPZero();
                }
            }
        }

        /// <summary>
        /// 返回距离该实体位置最近的游戏实体
        /// <list type="bullet">
        /// <item><param name="filter"><paramref name="filter"/>:寻找过滤器</param></item>
        /// <item><param name="rangeLimit"><paramref name="rangeLimit"/>:只会寻找到rangeLimit距离内的实体，负数表示无穷</param></item>
        /// </list></summary>
        /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
        public GameEntity NearestEntity(EntityFilter filter, float rangeLimit = -1)
        {
            return EntityMgr.NearestEntity(transform.position, filter, rangeLimit);
        }

        /// <summary>
        /// 返回距离该实体位置最近的游戏实体
        /// <list type="bullet">
        /// <item><param name="rangeLimit"><paramref name="rangeLimit"/>:只会寻找到rangeLimit距离内的实体，负数表示无穷</param></item>
        /// </list></summary>
        /// <returns>符合条件最近的实体，没有实体满足条件则返回null</returns>
        public GameEntity NearestEntity(float rangeLimit = -1)
        {
            return EntityMgr.NearestEntity(transform.position, rangeLimit);
        }

        protected virtual void OnHPZero()
        {
        }

        protected virtual void OnDead()
        {
            Register = false;
        }

        protected virtual void Awake()
        {
            Register = true;
        }

        protected virtual void OnDestroy()
        {
            Register = false;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            moveComponent = GetComponent<MoveComponent>();
            moveComponent = moveComponent != null ? moveComponent : gameObject.AddComponent<MoveComponent>();

            rotateComponent = GetComponent<RotateComponent>();
            rotateComponent = rotateComponent != null ? rotateComponent : gameObject.AddComponent<RotateComponent>();

            animationComponent = GetComponent<AnimationComponent>();
            animationComponent = animationComponent != null ? animationComponent : gameObject.AddComponent<AnimationComponent>();

            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider = sphereCollider != null ? sphereCollider : gameObject.AddComponent<SphereCollider>();

            animationComponent.SetDefaultClip("HumanIdle");
            animationComponent.EnableClipLoop("HumanRun");

            infos.HP = attrs.HPMax;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            foreach (var kv in spellUI)
            {
                var spell = kv.Key;
                var ui = kv.Value;
                ui.CoolingTimeSet = spell.coolingTimeSet;
                ui.CoolingTimeRemain = spell.CoolingTimeRemain;
            }

            healthBar.HPMax = attrs.HPMax;
            healthBar.CurrHP = infos.HP;
        }

        protected virtual void LateUpdate()
        {
            if (_deadState == DeadState.HPZero)
            {
                OnDead();
                _deadState = DeadState.Dead;
                EntityMgr.Release(this);
            }
        }
    }
}
