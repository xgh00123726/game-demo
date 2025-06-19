namespace GameBase.Spell
{
    // 圆形的技能指示器接口
    public interface IIndicatorCircleSpell : IIndicatorSpell
    {
        float Radius { get; set; }
    }
}
