using GameBase.Flyings;
using GameBase.Buffs;
using GameBase.Modify;
using Instance.Buffs;

namespace Constructor.Buffs
{
    public class BuffRegister
    {
        private static ViewableBuff BuffGen_0()
        {
            var buff = BuffSys.Instance.NewEntity<ViewableBuff>();
            buff.textureID = 0;
            buff.RegistertoActivesDelegate.Add(static (Buff b) =>
            {
                if (b.owner != null)
                {
                    if (b.owner is IModifyOwner<float> mOwner)
                    {
                        if (mOwner.Modifyables.Contains(0))
                        {
                            var modifyer = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                            modifyer.duration = b.durationSet;
                            modifyer.type = ModifyType.Aways | ModifyType.Temporary;
                            modifyer.ModifyFunc = ConvientModifyerFunc.FloatFixedValue(100);
                            mOwner.Modifyables.AddModify("coolingAccelerate", modifyer);

                            var msModifyer = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                            msModifyer.duration = b.durationSet;
                            msModifyer.type = ModifyType.Aways | ModifyType.Temporary;
                            msModifyer.ModifyFunc = ConvientModifyerFunc.FloatSetPercent(50);
                            mOwner.Modifyables.AddModify("moveSpeed", msModifyer);
                        }
                    }
                }
            });

            return buff;
        }

        public static void RegisterBuffGenerator()
        {
            BuffSys.Instance.RegisterEntityGenerateDeletate(0, BuffGen_0);
        }
    }
}
