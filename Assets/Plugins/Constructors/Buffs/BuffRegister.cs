using GameBase.Buffs;
using GameBase.Modify;

namespace Constructor.Buffs
{
    public class BuffRegister
    {
        private static Buff BuffGen_0()
        {
            var buff = BuffSys.Instance.NewEntity<Buff>();
            buff.textureID = 0;

            buff.RegistertoActivesDelegate += () =>
            {
                if (buff.owner is IModifyOwner<float> mOwner)
                {
                    if (mOwner.Modifyables.ContainsKey("coolingAccelerate"))
                    {
                        var modifyer = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                        modifyer.ModifyFunc = ConvientModifyerFunc.FloatFixedValue(100);
                        modifyer.duration = buff.durationSet;
                        modifyer.type = ModifyType.Aways | ModifyType.Temporary;

                        mOwner.Modifyables.AddModify("coolingAccelerate", modifyer);
                    }

                    if (mOwner.Modifyables.ContainsKey("moveSpeed"))
                    {
                        var msModifyer = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                        msModifyer.ModifyFunc = ConvientModifyerFunc.FloatSetPercent(50);
                        msModifyer.duration = buff.durationSet;
                        msModifyer.type = ModifyType.Aways | ModifyType.Temporary;

                        mOwner.Modifyables.AddModify("moveSpeed", msModifyer);
                    }
                }
            };


            buff.uiStyle = UIStyle.Buff;

            return buff;
        }

        public static void RegisterBuffGenerator()
        {
            BuffSys.Instance.RegisterEntityGenerateDeletate(0, BuffGen_0);
        }
    }
}
