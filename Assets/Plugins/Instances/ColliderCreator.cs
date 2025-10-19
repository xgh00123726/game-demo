using GameBase.EntitySystem;
using GameBase.Move;
using GameBase.Tools;
using UnityEngine;

namespace Instance
{
    public class ColliderCreator : SingletonInstance<ColliderCreator>
    {
        private static Rect _rectBuffer;
        public ColliderCreator()
        {
            RectDrawer.OnDraw += StackDrawRect;
            RectDrawer.OnDrawEnd += CreateRectCollider;
        }

        private static void CreateRectCollider()
        {
            if (!Instance.ActiveSelf)
            {
                return;
            }
            var c = CollideSys.Instance.NewEntity<FCRectCollider>();
            c.localPosition = _rectBuffer.center;
            c.w = _rectBuffer.width;
            c.h = _rectBuffer.height;
        }

        private static void StackDrawRect(Rect rect)
        {
            if (!Instance.ActiveSelf)
            {
                return;
            }

            _rectBuffer = rect;
        }
    }
}
