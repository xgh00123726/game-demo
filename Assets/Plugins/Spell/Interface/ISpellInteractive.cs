namespace GameBase.Spells
{
    public interface ISpellInteractive
    {
        bool IsTrig { get; }
        void Update(ISpeller speller);
        void OnTrig(ISpeller speller);
    }
}
