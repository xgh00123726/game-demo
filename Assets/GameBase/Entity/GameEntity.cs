using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Object;
using GameBase.Modify;
using GameBase.Spell;
using UnityEngine.Assertions;
using GameBase.UI;
using GameBase.Projectile;
using GameBase.Health;

namespace GameBase.Entity
{
    public class GameEntity : BaseEntity,
        IModifyable,
        ISpeller,
        IProjectileTarget,
        IProjectileOwner,
        IHealthBarOwner
    {
        public ModifyableAttrs attrs = new ModifyableAttrs();
        ModifyableAttrs IModifyable.attrs => attrs;

        float ISpeller.CoolingAccelerate => attrs.coolingAcclerate.Value;

        Transform ISpeller.Transform => transform;

        Vector3 IProjectileTarget.Center => transform.position + bodyOffset;

        float IProjectileTarget.Radius => sphereCollider.radius;

        Vector3 IProjectileOwner.HandPostion => transform.position + handOffset;

        Vector3 IHealthBarOwner.HealthBarPosition => transform.position + healthBarOffset;

        public MoveComponent moveComponent;
        public RotateComponent rotateComponent;
        public AnimationComponent animationComponent;
        public SphereCollider sphereCollider;

        public List<GSpell> spells = new List<GSpell>();
        public Dictionary<GSpell, SpellItem> spellUI = new Dictionary<GSpell, SpellItem>();

        public Vector3 handOffset = new Vector3(0, 1, 0);      // 手部偏移，纠正射弹射出位置
        public Vector3 bodyOffset = new Vector3(0, 1, 0);      // 身体偏移，纠正被射弹击中位置
        public Vector3 healthBarOffset = new Vector3(0, 3, 0); // 血条偏移，使血条放在人物头部

        public void AddSpell(GSpell spell, bool needUI = true)
        {
            spells.Add(spell);
            if (needUI)
            {
                spellUI[spell] = SpellPanel.Instance.AddItem();
            }
        }

        protected virtual void Awake()
        {
            EntityMgr.RegisterEntity(this);
        }

        protected virtual void OnDestroy()
        {
            EntityMgr.UnRegisterEntity(this);
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            moveComponent = GetComponent<MoveComponent>();
            moveComponent ??= gameObject.AddComponent<MoveComponent>();

            rotateComponent = GetComponent<RotateComponent>();
            rotateComponent ??= gameObject.AddComponent<RotateComponent>();

            animationComponent = GetComponent<AnimationComponent>();
            animationComponent ??= gameObject.AddComponent<AnimationComponent>();

            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider ??= gameObject.AddComponent<SphereCollider>();

            animationComponent.SetDefaultClip("HumanIdle");
            animationComponent.EnableClipLoop("HumanRun");

            HealthBarMgr.CreateHealthBar(this);
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
        }
    }
}
