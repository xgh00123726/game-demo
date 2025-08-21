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
    public class Common : EntityConstructor<CommonData, Effect, SimpleEntityContainer, EffectSys, Common>
    {
        protected override string RelativePath => "Effect/Common.csv";

        protected override EffectSys SysInstance => EffectSys.Instance;

        protected override void Parse(CsvReader line, ref CommonData data)
        {
            data.ObjID = int.Parse(line[1]);
            data.existTime = float.Parse(line[2]);
        }

        protected override void Set(Effect e, in CommonData data)
        {
            e.ObjID = data.ObjID;
            e.existTime = data.existTime;
        }
    }
}
