namespace GameBase.UI
{
    public interface IViewableSpell
    {
        float CoolingRemain { get; } // 当前持续时间
        float CoolingSet { get; }    // 最大持续时间
        int Charge { get; }       // 叠加层数

        int TextureID { get; }
    }
}
