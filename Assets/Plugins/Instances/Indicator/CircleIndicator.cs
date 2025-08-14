using GameBase.Indicators;
using GameBase.Spells;

namespace Instance.Indicators
{
    public class CircleIndicator : Indicator,
        IIndicator
    {
        public CircleIndicator()
        {
            ObjID = 19;
            textureID = 10;
        }

        void IIndicator.Hide()
        {
            visible = false;
        }

        void IIndicator.Show()
        {
            visible = true;
        }
    }
}
