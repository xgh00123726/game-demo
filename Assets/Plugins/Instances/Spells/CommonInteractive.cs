using GameBase.Tools;

namespace GameBase.Spells
{
    public class CommonInteractive : IInteractive
    {
        public CommonInteractive(KeyFunction readyKey,
            ISpellIndicator indicator = null,
            bool fastCast = false, 
            KeyFunction castKey = KeyFunction.MouseConfirm, 
            KeyFunction cancelKey = KeyFunction.Cancel)
        {
            _readyKey = readyKey;
            _castKey = castKey;
            _cancelKey = cancelKey;
            this.fastCast = fastCast;
            _indicator = indicator;
        }

        protected KeyFunction _readyKey;
        protected KeyFunction _castKey;
        protected KeyFunction _cancelKey;
        protected ISpellIndicator _indicator;
        public bool fastCast;

        bool IInteractive.ReadyTrig => fastCast ? true : Inputs.GetKeyDown(_readyKey);

        bool IInteractive.CastTrig => fastCast ? Inputs.GetKeyDown(_readyKey) : Inputs.GetKeyDown(_castKey);

        bool IInteractive.CancelTrig => Inputs.GetKeyDown(_cancelKey);

        void IInteractive.OnCancel()
        {
            _indicator?.Hide();
        }

        void IInteractive.OnCast()
        {
            _indicator?.Hide();
        }

        void IInteractive.OnReady()
        {
            _indicator?.Show();
        }

        void IInteractive.OnReadying(ISpeller speller)
        {
            _indicator?.Update(speller, GCamera.CameraSys.MouseHitPosition);
        }
    }
}
