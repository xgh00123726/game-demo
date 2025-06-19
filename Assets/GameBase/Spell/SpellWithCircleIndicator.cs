namespace GameBase.Spell
{
    public class SpellWithCircleIndicator : SpellWithIndicator
    {
        protected float _radius;
        protected new IIndicatorCircleSpell _indicator;
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
