namespace GameBase.Spell
{
    /// <summary>
    /// 带有指示器的技能
    /// </summary>
    public class SpellWithIndicator : GSpell
    {
        protected IIndicatorSpell _indicator;

        public SpellWithIndicator(ISpeller speller, IIndicatorSpell indicator) : base(speller)
        {
            _indicator = indicator;
        }

        protected override void OnCancel()
        {
            _indicator.Hide();
        }

        protected override void OnCast()
        {
            _indicator.Hide();
        }

        protected override void OnChoose()
        {
            _indicator.Show();
        }

        protected override void OnChoosing()
        {
            _indicator.Move();
        }
    }
}
