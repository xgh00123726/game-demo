using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Spell
{
    public class SpellMgr : MonoBehaviour
    {
        private static List<GSpell> _spells = new List<GSpell>();

        public static void Addspell(GSpell spell)
        {
            _spells.Add(spell);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            foreach (GSpell spell in _spells)
            {
                spell.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (var spell in _spells)
            {
                spell.FixedUpdate();
            }
        }
    }
}
