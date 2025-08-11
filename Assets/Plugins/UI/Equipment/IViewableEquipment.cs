namespace GameBase.UI
{
    public interface IViewableEquipment
    {
        float DurationRemain { get; } // 当前持续时间
        float DurationSet { get; }    // 最大持续时间
        int StackNum { get; }       // 叠加层数
        int TextureID { get; }
        int Position { get; }
    }
}
