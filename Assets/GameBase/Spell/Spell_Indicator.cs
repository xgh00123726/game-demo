namespace GameBase.Spell
{
    public partial class GSpell
    {
        protected IIndicatorSpell _indicator;
        public bool _hasIndicator = false;

        public IIndicatorSpell Indicator
        {
            get => _indicator;
            set
            {
                if (value == null) return;

                _indicator = value;
                _hasIndicator = true;
            }
        }
    }
}
