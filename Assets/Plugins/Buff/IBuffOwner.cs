using GameBase.Modify;

namespace GameBase.Buffs
{
    public interface IBuffOwner : IModifyOwner<float>
    {
        BuffContainer Buffs { get; }
    }
}
