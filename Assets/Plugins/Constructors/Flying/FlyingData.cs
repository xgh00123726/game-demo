using GameBase.Flyings;
using UnityEngine;

namespace Constructor.Flyings
{
    public class FlyingData
    {
        public CurveData Curve { get; set; }
        public string PrefabName { get; set; }
        public float Speed { get; set; }
        public float MinExistTime {  get; set; }
        public string ReleaseEffectName { get; set; }
    }
}
