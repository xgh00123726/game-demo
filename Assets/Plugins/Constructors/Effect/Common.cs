using GameBase.EntitySystem;
using GameBase.Effects;
using NReco.Csv;

namespace Constructor.Effects
{
    public struct CommonData
    {
        public int ObjID;
        public float existTime;
    }
    public class Common : EntityConstructor<CommonData, Effect, EffectSys, Common>
    {
        protected override string RelativePath => "Effect/Common.csv";

        protected override EffectSys SysInstance => EffectSys.Instance;

        protected override void ESet(Effect e, in CommonData data)
        {
            e.ObjID = data.ObjID;
            e.existTime = data.existTime;
        }
    }
}
