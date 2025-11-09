using GameBase.Flyings;
using UnityEngine;

namespace Constructor.Flyings
{
    public class FlyingData
    {
        public string prefabName;
        public float speed;
        public float minExistTime;
        public CurveFactory.CurveType curveType;
        public string releaseEffectName;
        public string hitEffectName;
    }
}
