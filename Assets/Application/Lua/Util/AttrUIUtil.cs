using GameBase.UI;

namespace LuaUtil
{
    public static class AttrUIUtil
    {
        private static AttrViewPanel _panel;

        public static void Init()
        {
            _panel = new AttrViewPanel();

            _panel.Show();
        }

        public static void SetAttrKey(int key, int index)
        {
            _panel.SetAttrKey(key, index);
        }

        public static void SetLayout(float xInterval, float yInterval, float width, float height, int align)
        {
            _panel.layout = new DefaultLayout()
            {
                xInterval = xInterval,
                yInterval = yInterval,
                width = width,
                height = height,
                align = (AlignType)align
            };
        }

        public static void SetValue(int creatureID)
        {
            var c = CreatureSys.Instance.GetCreature(creatureID);

            if (c != null) 
            {
                _panel.SetAttrValue(c);
            }
        }
    }
}
