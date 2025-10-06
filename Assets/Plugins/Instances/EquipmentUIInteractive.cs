using GameBase.Creatures;
using GameBase.EntitySystem;

namespace Instance
{
    public class EquipmentUIInteractive : UIInteractive<EquipmentUIInteractive, EquipmentPanel, EquipmentItem>
    {
        private static Creature _target;

        protected override EquipmentPanel GetPanel()
        {
            return EquipmentPanel.Instance;
        }

        public static void SetTarget(Creature target)
        {
            _target = target;
        }

        protected override void Update()
        {
            base.Update();
            if (_target != null)
            {
                var panel = GetPanel();
                panel.UpdatePanel(_target);
            }
        }
    }
}
