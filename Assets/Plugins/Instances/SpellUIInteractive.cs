using GameBase.Creatures;

namespace Instance
{
    public class SpellUIInteractive : UIInteractive<SpellUIInteractive, SpellViewPanel, SpellViewItem>
    {
        public Creature Target {  get; set; }
        protected override SpellViewPanel GetPanel()
        {
            return SpellViewPanel.Instance;
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
