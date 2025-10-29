using GameBase.Resources;
using UnityEngine;

namespace Instance
{
    public class MoveIndicator
    {
        public static void Show(Vector3 position)
        {
            EffectSys.Instance.PlayAtP("Prefabs/Effect/Move/RightClickEffect", position);
        }
    }
}
