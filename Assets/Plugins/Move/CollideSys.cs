using GameBase.EntitySystem;
using GameBase.Tools;
using System;
using UnityEngine;

namespace GameBase.Move
{
    public class CollideSys : InheritableSys<Collider, CollideSys>
    {
        protected ListContainer<Collider> colliders = new();
        public Action<Grid> OnGridDirty;
        public Grid grid;
        public CollideSys()
        {
            _sys.Entities = colliders;
            grid = new Grid(new Rect()
            {
                width = 100,
                height = 100,
                center = new Vector2(-6, 4)
            }, new Vector2(0.5f, 0.5f));
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
            base.Update();
            StateReset();
            CollideJug();
        }

        protected override void OnGet(Collider e)
        {
            e.hasOwner = false;
        }

        protected override void EntityStart(Collider e)
        {
            if (e is RectCollider rc)
            {
                rc.Update();
                grid.UpdateMap(Rect.MinMaxRect(rc.XMin, rc.YMin, rc.XMax, rc.YMax), 255f);
                OnGridDirty?.Invoke(grid);
            }
        }

        protected override void ReleaseEntity<T>(T e)
        {
            if (e is RectCollider rc)
            {
                rc.Update();
                grid.UpdateMap(Rect.MinMaxRect(rc.XMin, rc.YMin, rc.XMax, rc.YMax), 1f);
                OnGridDirty?.Invoke(grid);
            }
        }

        protected override void UpdateEntity(Collider e)
        {
            e.Update();
        }
    }
}
