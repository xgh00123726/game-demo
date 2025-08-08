using GameBase.Projectile;
using GameBase.Buffs;
using GameBase.Modify;
using System;
using Instance.Buffs;

namespace Constructor.Buffs
{
    public class BuffRegister
    {
        private static float Modify_0(float v)
        {
            return v + 100;
        }

        private static ViewableBuff BuffGen_0()
        {
            var buff = BuffSys.Instance.NewEntity<ViewableBuff>();
            buff.InstantiateDelegate = static (Buff b) =>
            {
                if (b.owner.Exist)
                {
                    if (b.owner.Get() is IModifyOwner<float> mOwner)
                    {
                        if (mOwner.Modifyables.Contains(0))
                        {
                            mOwner.Modifyables[0].AddModify(Modify_0);
                        }
                    }
                }
            };
            buff.ReleaseDelegate = static (Buff b) =>
            {
                if (b.owner.Exist)
                {
                    if (b.owner.Get() is IModifyOwner <float> mOwner)
                    {
                        if (mOwner.Modifyables.Contains(0))
                        {
                            mOwner.Modifyables[0].RemoveModify(Modify_0);
                        }
                    }
                }
            };

            return buff;
        }

        public static void RegisterBuffGenerator()
        {
            BuffSys.Instance.RegisterEntityGenerateDeletate(BuffGen_0);
        }
    }
}
