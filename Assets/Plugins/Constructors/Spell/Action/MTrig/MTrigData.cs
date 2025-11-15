using Constructor.Triggers;

namespace Constructor.Spells
{
    public enum TrigPositionStyle
    {
        Mouse,
        Self
    }
    public class MTrigData
    {
        public TrigPositionStyle trigPositionStyle;
        public TriggerData trigger;
        public int slotNum;
    }
}
