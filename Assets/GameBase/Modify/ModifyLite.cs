using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyLite : GModify
    {
        public float coolingAcclerate;

        protected override void OnModify()
        {
            _target.attrs.coolingAcclerate.Value += coolingAcclerate;
        }
    }
}
