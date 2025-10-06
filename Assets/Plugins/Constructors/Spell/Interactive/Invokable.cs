using GameBase.Indicators;
using GameBase.Spells;
using GameBase.Tools;

namespace Constructor.Spells.Interactive
{
    public class Invokable : ISpellInteractive
    {
        public IndicatorType indicatorType;
        public float indicatorLength = 10;
        public float indicatorRadius;

        private bool _isTrig = false;

        bool ISpellInteractive.IsTrig => _isTrig;

        public bool Invoke()
        {
            _isTrig = true;

            return true;
        }

        public static void Invoke(ISpellInteractive interactive)
        {
            if (interactive is Invokable e)
            {
                e.Invoke();
            }
        }

        public static Invokable Get(int id)
        {
            return new Invokable();
        }

        void ISpellInteractive.OnTrig()
        {
            _isTrig = false;
        }
    }
}
