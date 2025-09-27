namespace Instance
{
    public enum SecondType
    {
        None = 0,
        Equipment,
        SpellActionModifier,
    }
    public class CommonInventoryData
    {
        public SecondType type;
        public int secondID;
    }
}
