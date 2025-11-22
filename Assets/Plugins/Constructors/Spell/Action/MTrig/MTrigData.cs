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
        public TrigPositionStyle TrigPositionStyle { get; set; }
        public TriggerData Trigger {  get; set; }
        public int SlotNum { get; set; }
    }
}
