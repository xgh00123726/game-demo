using GameBase.Projectile;
using GameBase.Buffs;
using GameBase.Modify;
using Instance.Buffs;

namespace Constructor.Buffs
{
    public class BuffRegister
    {
        private static float Modify_0(float val, float valSet)
        {
            return val + 100;
        }

        private static ViewableBuff BuffGen_0()
        {
            var buff = BuffSys.Instance.NewEntity<ViewableBuff>();
            buff.textureID = 0;
            buff.RegistertoActivesDelegate.Add(static (Buff b) =>
            {
                if (b.owner.Exist)
                {
                    if (b.owner.Get() is IModifyOwner<float> mOwner)
                    {
                        if (mOwner.Modifyables.Contains(0))
                        {
                            var modifyer = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                            modifyer.duration = b.durationSet;
                            modifyer.ModifyFunc = Modify_0;
                            mOwner.Modifyables.AddModify(12, modifyer);

                            var msModifyer = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                            msModifyer.duration = b.durationSet;
                            msModifyer.ModifyFunc = static (float val, float valSet) => { return val + valSet * 0.5f; };
                            mOwner.Modifyables.AddModify(1, msModifyer);
                        }
                    }
                }
            });

            return buff;
        }

        public static void RegisterBuffGenerator()
        {
            BuffSys.Instance.RegisterEntityGenerateDeletate(BuffGen_0);
        }
    }
}
