using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creatures
{
    public partial class Creature :
        ISpeller
    {
        public DynInventory<Spell> spells = new();

        float ISpeller.CoolingAccelerate => modifyables["coolingAccelerate"].Value;

        ISpellCamp ISpeller.Camp => camp;

        public void AddSpell(Spell spell)
        {
            var i = spells.Add(spell);
            XLogger.Instance.IF(false).Log($"add spell: {i}");
            spell.speller = this;
        }

        public Spell GetSpell(int index)
        {
            if (spells.HasItem(index))
            {
                return spells[index];
            }

            return null;
        }

        void ISpeller.LookAt(Vector3 pos)
        {
            Interrupt();
            mover.LookAt(pos);
        }
    }
}
