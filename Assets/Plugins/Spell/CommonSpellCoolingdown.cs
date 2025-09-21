using UnityEngine;

namespace GameBase.Spells
{
    public class CommonSpellCoolingdown : ISpellCoolingdown
    {
        private bool _isCoolingOver = false;
        private float _coolingSet = 10;
        private float _coolingRemain = 0;

        bool ISpellCoolingdown.IsCoolingOver => _isCoolingOver;

        float ISpellCoolingdown.CoolingSet
        {
            get => _coolingSet;
            set => _coolingSet = value;
        }

        float ISpellCoolingdown.CoolingRemain => _coolingRemain;

        void ISpellCoolingdown.Recooling()
        {
            _coolingRemain = 0;
        }

        void ISpellCoolingdown.Update(float coolingAccelerate)
        {
            float acc = 1;
            acc = coolingAccelerate * 0.01f + 1;
            _coolingRemain -= Time.deltaTime * acc;
        }
    }
}
