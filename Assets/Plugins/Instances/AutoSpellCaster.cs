using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Spells;
using GameBase.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Instance
{
    public class AutoSpellCaster : SingletonInstance<AutoSpellCaster>
    {
        private static List<Spell> _spells = new();

        public static void Register(Spell spell)
        {
            _spells.Add(spell);
        }

        public static void Unregister(Spell spell)
        {
            _spells.Remove(spell);
        }
        public static void CastByStyle(Spell spell)
        {
            var c = CreatureSys.Instance.NearestEntity(spell.speller.Position, (GameBase.Creatures.CampType)spell.camp);
            if (c != null)
            {
                spell.castPosition = c.Position;
            }
            spell.TryCast();
        }

        protected override void Update()
        {
            foreach (var spell in _spells)
            {
                spell.TryCast();
            }
        }
    }
}
