using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Object;
using GameBase.Modify;
using GameBase.Spell;
using UnityEngine.Assertions;
using GameBase.UI;

namespace GameBase.Entity
{
    public class GameEntity : BaseEntity,
        IModifyable,
        ISpeller
    {
        public ModifyableAttrs attrs = new ModifyableAttrs();
        ModifyableAttrs IModifyable.attrs => attrs;

        float ISpeller.CoolingAccelerate => attrs.coolingAcclerate.Value;

        public MoveComponent moveComponent;
        public RotateComponent rotateComponent;
        public AnimationComponent animationComponent;
        public SphereCollider sphereCollider;

        public List<GSpell> spells = new List<GSpell>();
        public Dictionary<GSpell, SpellItem> spellUI = new Dictionary<GSpell, SpellItem>();

        public void AddSpell(GSpell spell, bool needUI = true)
        {
            spells.Add(spell);
            if (needUI)
            {
                spellUI[spell] = SpellPanel.Instance.AddItem();
            }
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
