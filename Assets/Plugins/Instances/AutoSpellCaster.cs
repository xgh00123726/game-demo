using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Spells;
using System.Collections.Generic;

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

        protected override void Update()
        {
            foreach (var spell in _spells)
            {
                spell.TryCast();
            }
        }
    }
}
