namespace GameBase.Spells
{
    public interface IInteractive
    {
        bool ReadyTrig { get; }
        bool CastTrig { get; }
        bool CancelTrig { get; }
        void OnReadying(ISpeller speller);
        void OnCancel();
        void OnReady();
        void OnCast();
    }
}
