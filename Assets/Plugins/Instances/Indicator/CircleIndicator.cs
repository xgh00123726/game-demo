using GameBase.Indicators;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace Instance.Indicators
{
    public class CircleIndicator : Indicator,
        ISpellIndicator
    {
        public CircleIndicator()
        {
            ObjID = 19;
            textureID = 10;
        }

        void ISpellIndicator.Hide()
        {
            Obj.SetActive(false);
        }
        void ISpellIndicator.Update(ISpeller speller, Vector3 position)
        {
            Obj.transform.position = position;
        }
        void ISpellIndicator.Show()
        {
            Obj.SetActive(true);
        }
    }
}
