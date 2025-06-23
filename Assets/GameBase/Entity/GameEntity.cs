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
            Assert.IsNotNull(moveComponent);

            rotateComponent = GetComponent<RotateComponent>();
            Assert.IsNotNull(rotateComponent);

            animationComponent = GetComponent<AnimationComponent>();
            Assert.IsNotNull(animationComponent);
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
