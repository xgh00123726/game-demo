namespace GameBase.Triggers
{
    public interface ITriggerCamp
    {
        ITriggerCamp And(ITriggerCamp other);
        ITriggerCamp Or(ITriggerCamp other);
        bool IsNone();
        uint ToUint();
    }

    public interface ITriggerCampSet
    {
        ITriggerCamp GetCamp(ITriggerCamp baseCamp);
    }
}
