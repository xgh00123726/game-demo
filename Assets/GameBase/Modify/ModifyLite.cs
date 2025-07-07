using UnityEngine;

namespace GameBase.Modify
{
    /// <summary>
    /// 一个包含所有common Modify的类，用于修改基础属性
    /// </summary>
    public class ModifyLite : Modify
    {
        public float coolingAcclerate;
        public float health;
        public float damage;
        public float defense;

        protected override void OnModify()
        {
            _target.attrs.coolingAcclerate.Value += coolingAcclerate;
            _target.attrs.HPMax.Value += health;
            _target.attrs.damage.Value += damage;
            _target.attrs.defense.Value += defense;
        }
    }
}
