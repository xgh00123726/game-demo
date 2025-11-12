namespace GameBase.Spells
{
    public interface ISpellCamp
    {
        ISpellCamp And(ISpellCamp other);
        ISpellCamp Or(ISpellCamp other);
        bool IsNone();
        uint ToUint();
    }

    public interface ISpellCampSet
    {
        ISpellCamp GetCamp(ISpellCamp baseCamp);
    }
}
