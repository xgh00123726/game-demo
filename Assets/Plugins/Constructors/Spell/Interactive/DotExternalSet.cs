using GameBase.Indicators;
using GameBase.Spells;
using System;
using UnityEngine;

namespace Constructor.Spells.Interactive
{
    public class DotExternalSet : ISpellInteractive, IDotInput
    {
        public IndicatorType indicatorType;
        public float length = 10;
        public float radius;

        public Func<Vector3> GetPosition;

        private Vector3 _position;

        public Vector3 Position => _position;

        public static DotExternalSet Get(int id)
        {
            return new DotExternalSet();
        }

        void ISpellInteractive.OnTrig()
        {
            if (GetPosition != null)
            {
                _position = GetPosition();
            }
        }
    }
}
