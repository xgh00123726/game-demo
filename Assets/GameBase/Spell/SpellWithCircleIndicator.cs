namespace GameBase.Spell
{
    /// <summary>
    /// 带有圆形指示器的技能
    /// </summary>
    public class SpellWithCircleIndicator : SpellWithIndicator
    {
        protected float _radius;
        protected new IIndicatorCircleSpell _indicator;
        /// <summary><list type="bullet">
        /// <item>get: 获取指示器半径</item>
        /// <item>set: 设置指示器半径</item>
        /// </list></summary>
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                _indicator.Radius = value;
            }
        }
        public SpellWithCircleIndicator(ISpeller speller, IIndicatorCircleSpell indicator) : base(speller, indicator)
        {
            _indicator = indicator;
        }
    }
}
