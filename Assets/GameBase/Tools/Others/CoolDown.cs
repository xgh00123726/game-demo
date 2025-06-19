using UnityEngine;
namespace GameBase.Tools
{
    public partial class CoolDown
    {
        protected float _coolDowmTimeSet = 1f;
        public float CoolDownTimeSet
        {
            get => _coolDowmTimeSet;
            set => _coolDowmTimeSet = value;
        }
        public float ActualCoolDownTime => _coolDowmTimeSet * Mathf.Max(0, (1 - _coolDowmTimeReduce));
        protected float _coolingAccelerate = 0f;
        public float CoolingAccelerate
        {
            get => _coolingAccelerate;
            set => _coolingAccelerate = value;
        }

        protected float _coolDowmTimeReduce = 0f;
        public float CoolingDowmTimeReduce
        {
            get => _coolDowmTimeReduce;
            set => _coolDowmTimeReduce = value;
        }

        protected float _coolDownBeginTime = -999999f;

        public float CoolTimeRemain => Mathf.Max(0, ActualCoolDownTime - (Time.time - _coolDownBeginTime) * (1 + _coolingAccelerate));
        public float CoolPercentRemain
        {
            get
            {
                if (ActualCoolDownTime <= 0f) return 0;
                return Mathf.Max(0, 1 - (Time.time - _coolDownBeginTime) * (1 + _coolingAccelerate) / ActualCoolDownTime);
            }
        }
        public bool IsCoolOver => (Time.time - _coolDownBeginTime) * (1 + _coolingAccelerate) >= ActualCoolDownTime;

        public void Begin()
        {
            _coolDownBeginTime = Time.time;
        }

    }
}
