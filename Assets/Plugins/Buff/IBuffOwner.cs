using GameBase.Modify;
using GameBase.Tools;

namespace GameBase.Buffs
{
    public interface IBuffOwner : IModifieder
    {
        void OnGetBuff(Buff buff);
        void OnRemoveBuff(Buff buff);
    }
}
