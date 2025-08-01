namespace GameBase.Buff
{
    public interface IViewableBuff
    {
        float DurationRemain { get; } // 当前持续时间
        float DurationSet { get; }    // 最大持续时间
        int StackNum { get; }       // 叠加层数
    }
}
