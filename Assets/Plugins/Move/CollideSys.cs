using GameBase.EntitySystem;
using GameBase.Math;
using UnityEngine;

namespace GameBase.Move
{
    public class CollideSys : CommonEntitySys<Collider, CollideSys>
    {
        protected ListContainer<Collider> colliders = new();

        public override IEContainer<Collider> Entities => colliders;

        protected void PositionUpdate()
        {
            foreach (var c in Entities)
            {
                c.position = new UnityEngine.Vector2(c.owner.position.x, c.owner.position.z);
            }
        }

        protected void StateReset()
        {
            foreach (var c in Entities)
            {
                c.isCollide = false;
                c.force = Vector2.zero;
            }
        }

        protected void CollideJug()
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                var c1 = colliders[i];
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    var c2 = colliders[j];

                    var x1 = c1.position.x;
                    var y1 = c1.position.y;
                    var x2 = c2.position.x;
                    var y2 = c2.position.y;
                    var r1 = c1.r;
                    var r2 = c2.r;

                    float dx = x2 - x1;
                    float dy = y2 - y1;
                    var force = new Vector2(dx, dy);
                    var forceLen = force.magnitude;
                    float intersectLen = r1 + r2 - forceLen;

                    if (intersectLen > 0)
                    {
                        c1.isCollide = true;
                        c2.isCollide = true;

                        c1.force += force.normalized * -intersectLen;
                        c2.force += force.normalized * intersectLen;
                    }
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
