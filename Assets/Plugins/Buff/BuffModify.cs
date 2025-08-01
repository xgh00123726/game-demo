using GameBase.Modify;
using GameBase.Object;
using UnityEngine;

namespace GameBase.Buff
{
    public class BuffModify : GBuff
    {
        private ModifyLite _modify;
        public BuffModify(GObject caster, ModifyLite modify) : base(caster)
        {
            _modify = modify;
        }
        

        protected override void OnCast()
        {
            _modify.ModifyTo(_target);
        }

        protected override void OnClear()
        {
            _modify.Destroy();
        }
    }
}
