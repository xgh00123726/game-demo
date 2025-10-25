using GameBase.Creatures;
using GameBase.EntitySystem;

namespace Instance
{
    public class EquipmentUIInteractive : UIInteractive<EquipmentUIInteractive, EquipmentPanel, EquipmentItem>
    {
        public Creature Target {  get; set; }

        protected override EquipmentPanel GetPanel()
        {
            return EquipmentPanel.Instance;
        }

        protected override void Update()
        {
            base.Update();
            if (Target != null)
            {
                var panel = GetPanel();
                panel.UpdatePanel(Target);
            }
        }
    }
}
