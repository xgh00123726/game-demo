using UnityEngine;

namespace Instance
{
    public class MoveIndicator
    {
        public static void Show(Vector3 position)
        {
            var effect = Constructor.Effects.Factory.Instance.Get(Constructor.Effects.Type.Common, 2);
            effect.Obj.transform.position = position;
        }
    }
}
