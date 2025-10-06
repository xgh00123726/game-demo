using GameBase.Creatures;

namespace Instance
{
    public class SpellUIInteractive : UIInteractive<SpellUIInteractive, SpellViewPanel, SpellViewItem>
    {
        private static Creature _target;
        protected override SpellViewPanel GetPanel()
        {
            return SpellViewPanel.Instance;
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
