using UnityEngine;
using GameBase.Tools;


namespace GameBase.Spell
{
    public partial class GSpell
    {
        public float CoolingTimeRemain => _coolingTimeRemain > 0 ? _coolingTimeRemain : 0;
        public float CoolingTimeRemainPercent => CoolingTimeRemain / coolingTimeSet;
        public bool IsCoolDown => _coolingTimeRemain <= 0;
        private SequentialBool _isChoosing = (SequentialBool)false;
        public virtual bool IsChoosing => isTargetable ? _isChoosing : true;
        private void CoolDownFixedUpdate()
        {
            if (_coolingTimeRemain > 0)
            {
                _coolingTimeRemain -= Time.fixedDeltaTime
                    * (_speller.CoolingAccelerate * 0.01f + 1);
            }
        }
    }
}
