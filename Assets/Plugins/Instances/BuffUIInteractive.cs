using GameBase.Creatures;

namespace Instance
{
    public class BuffUIInteractive : UIInteractive<BuffUIInteractive, BuffViewPanel, BuffViewItem>
    {
        private static Creature _target;
        protected override BuffViewPanel GetPanel()
        {
            return BuffViewPanel.Instance;
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
