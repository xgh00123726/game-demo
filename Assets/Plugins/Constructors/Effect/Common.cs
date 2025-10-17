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
    public class Common : KeyConstructor<CommonData, Effect, Common>
    {
        protected override string RelativePath => "Effect/Common.csv";
        protected override Effect GetFromData(in CommonData data)
        {
            var e = EffectSys.Instance.NewEntity(data.ObjID);
            e.existTime = data.existTime;
            return e;
        }
    }
}
