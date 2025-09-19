using GameBase.Spells;

namespace Constructor.Spells.Interactive
{
    public class Invokable : ISpellInteractive
    {
        private bool _isTrig = false;
        bool ISpellInteractive.IsTrig => _isTrig;

        void ISpellInteractive.OnTrig(ISpeller speller)
        {
            _isTrig = false;
        }

        void ISpellInteractive.Update(ISpeller speller)
        {
            if (_isTrig)
            {
                _isTrig = false;
            }
        }

        public void Invoke()
        {
            _isTrig = true;
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
    }
}
