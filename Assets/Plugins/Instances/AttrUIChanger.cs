using GameBase.Creatures;
using GameBase.EntitySystem;

namespace Instance
{
    public class AttrUIChanger : SingletonInstance<AttrUIChanger>
    {
        private static Creature _target = null;

        protected override void Update()
        {
            if (_target == null)
            {
                return;
            }

            var panel = AttrViewPanel.Instance;
            if (panel == null)
            {
                return;
            }

            panel.SetAttrValue(_target);
        }

        public static void SetTarget(Creature target)
        {
            _target = target;
        }
    }
}
