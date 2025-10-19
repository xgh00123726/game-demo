using UnityEngine;

namespace GameBase.Spells
{
    public class CommonSpellCoolingdown : ISpellCoolingdown
    {
        private bool _isCoolingOver = false;
        private float _coolingSet = 10;
        private float _coolingRemain = 0;

        public bool IsCoolingOver => _isCoolingOver;

        public float CoolingSet
        {
            get => _coolingSet;
            set => _coolingSet = value;
        }

        public float CoolingRemain => _coolingRemain;

        void ISpellCoolingdown.Recooling()
        {
            _coolingRemain = _coolingSet;
        }

        void ISpellCoolingdown.Update(float coolingAccelerate)
        {
            float acc = 1;
            acc = coolingAccelerate * 0.01f + 1;
            if (_coolingRemain > 0)
            {
                _coolingRemain -= Time.deltaTime * acc;
                _isCoolingOver = false;
            }
            if (_coolingRemain <= 0)
            {
                _isCoolingOver = true;
            }
        }
    }
}
