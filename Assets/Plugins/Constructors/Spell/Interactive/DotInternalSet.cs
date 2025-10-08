using GameBase.Indicators;
using GameBase.Spells;
using UnityEngine;

namespace Constructor.Spells.Interactive
{
    public class DotInternalSet : ISpellInteractive, IDotInput
    {
        public IndicatorType indicatorType;
        public float length = 10;
        public float radius;

        public Vector3 position;

        public Vector3 Position => position;

        public static DotInternalSet Get(int id)
        {
            return new DotInternalSet();
        }

        void ISpellInteractive.OnTrig()
        {
        }
    }
}
