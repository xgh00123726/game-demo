using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Indicators;
using GameBase.Spells;
using System;
using UnityEngine;

namespace Constructor.Spells.Interactive
{
    public enum DotExternalSetStyle
    {
        None,
        NearestTarget,
        Random,
    }
    public struct DotExternalSetData
    {
        public DotExternalSetStyle style;
        public CreatureTag targetTag;
        public float randomMinX;
        public float randomMaxX;
        public float randomMinY;
        public float randomMaxY;
        public float randomMinZ;
        public float randomMaxZ;
    }
    public class DotExternalSet : ISpellInteractive, IDotInput
    {
        public DotExternalSetData data;
        public IndicatorType indicatorType;
        public float length = 10;
        public float radius;

        public Func<Vector3> GetPosition;

        public Vector3 position;

        public Vector3 Position => position;

        void ISpellInteractive.OnTrig()
        {
            if (GetPosition != null)
            {
                position = GetPosition();
            }
        }
    }

    public class DotExternalSetCon : BaseConstructor<DotExternalSetData, DotExternalSet, DotExternalSetCon>
    {
        protected override string RelativePath => "Spell/Interactive/DotExternalSet.csv";

        protected override DotExternalSet Get()
        {
            return new DotExternalSet();
        }

        protected override void Set(DotExternalSet e, in DotExternalSetData data)
        {
            e.data = data;
        }
    }
}
