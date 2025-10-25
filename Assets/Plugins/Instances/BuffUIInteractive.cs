using GameBase.Creatures;

namespace Instance
{
    public class BuffUIInteractive : UIInteractive<BuffUIInteractive, BuffViewPanel, BuffViewItem>
    {
        public Creature Target {  get; set; }
        protected override BuffViewPanel GetPanel()
        {
            return BuffViewPanel.Instance;
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
