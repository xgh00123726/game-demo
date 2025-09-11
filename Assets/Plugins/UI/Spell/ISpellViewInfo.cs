namespace GameBase.UI
{
    public interface ISpellViewInfo
    {
        float CoolingRemain { get; } // 当前持续时间
        float CoolingSet { get; }    // 最大持续时间
    }
}
