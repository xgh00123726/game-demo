using GameBase.Creatures;
using GameBase.Spells;
using GameBase.UI;

namespace Instance
{
    public class SpellActionModifierInteractive : UIInteractive<SpellActionModifierInteractive, SpellActionModifierViewPanel, SpellActionModifierViewItem>
    {
        public Spell Target { get; set; }
        protected override SpellActionModifierViewPanel GetPanel()
        {
            return SpellActionModifierViewPanel.Instance;
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
