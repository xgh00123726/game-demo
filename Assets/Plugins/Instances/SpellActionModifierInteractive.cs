using GameBase.Creatures;
using GameBase.Spells;
using GameBase.UI;

namespace Instance
{
    public class SpellActionModifierInteractive : UIInteractive<SpellActionModifierInteractive, SpellActionModifierViewPanel, SpellActionModifierViewItem>
    {
        private static Spell _target;
        protected override SpellActionModifierViewPanel GetPanel()
        {
            return SpellActionModifierViewPanel.Instance;
        }

        public static void SetTarget(Spell target)
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
