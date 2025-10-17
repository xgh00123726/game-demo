using GameBase.EntitySystem;
using GameBase.Math;
using UnityEngine;

namespace GameBase.Move
{
    public class CollideSys : InheritableSys<Collider, CollideSys>
    {
        protected ListContainer<Collider> colliders = new();

        public override IEContainer<Collider> Entities => colliders;

        private void PositionUpdate()
        {
            foreach (var c in Entities)
            {
                c.position = new UnityEngine.Vector2(c.owner.position.x, c.owner.position.z);
            }
        }

        private void StateReset()
        {
            foreach (var c in Entities)
            {
                c.isCollide = false;
                c.force = Vector2.zero;
            }
        }

        private void CollideJug()
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    colliders[i].CollideTo(colliders[j]);
                }
            }
        }

        protected override void Update()
        {
            PositionUpdate();
            StateReset();
            CollideJug();
        }

        protected override void UpdateEntity(Collider e)
        {
            
        }
    }
}
