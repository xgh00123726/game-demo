namespace GameBase.Spells
{
    public interface ISpellCoolingdown
    {
        bool IsCoolingOver { get; }
        float CoolingSet { get; set; }
        float CoolingRemain { get;}
        void Update(float coolingAccelerate);
        void Recooling();
    }
}
