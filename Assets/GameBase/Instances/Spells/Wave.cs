using UnityEngine;
using GameBase.Entity;
using GameBase.GCamera;
using GameBase.Projectile;
using GameBase.Tools;
using GameBase.UI;
using UnityEditor.Experimental.GraphView;
using Logger = GameBase.Tools.Logger;
using System.Collections.Generic;

namespace GameBase.Spell
{
    public class Wave : GSpell
    {
        public int exploreNum = 10;   // 实例数量
        public int exploreIndex = 0;  // 当前实例的index
        public float distance = 10f;  // 行进距离
        public float width = 1f;   // 技能宽度
        public float speed = 10f;  // 行进速度
        public Vector3 dir = Vector3.one;   // 行进方向
        private Vector3 _actualDest = Vector3.zero; // 实际距离
        public KeyFunction trigKey = KeyFunction.None; // 热键
        public float time = 1f;
        public LinkedList<IProjectileTarget> _whites = new LinkedList<IProjectileTarget>();

        public Wave()
        {
            ChooseCondition = (GSpell spell) => Inputs.GetKeyDown(trigKey);
            CancelCondition = (GSpell spell) => Inputs.GetKeyDown(KeyFunction.Cancel);
            InvokeCondition = (GSpell spell) => spell.IsChoosing
                && Inputs.GetKeyDown(KeyFunction.MouseConfirm);
        }

        private void InstanceExplore()
        {
            var entitys = EntityMgr.EntityWithin(PlayerCamera.MouseHitPoint, width / 2, (GameEntity e) =>
            {
                return e.camp == GameEntity.Camp.Rival;
            });
            var proj = ProjectileMgr<GameBase.Projectile.Explore>.Instance.CreateProjectile();

            foreach (var entity in _whites)
            {
                proj.AddWhite(entity);
            }

            if (_speller is IProjectileOwner owner)
            {
                proj.Owner = owner;
            }
            proj.damage = effective;
            proj.HitTargetEventAction = (IProjectileTarget target) =>
            {
                _whites.AddLast(target);
            };
            proj.Dest = Vector3.Lerp(src, _actualDest, ((float)exploreIndex) / exploreNum);

            if (exploreIndex++ >= exploreNum)
            {
                OnWaveSpellEnd();
            }
        }

        private void OnWaveSpellEnd()
        {
            exploreIndex = 0;
        }

        protected override void OnCast()
        {
            base.OnCast();

            src = _speller.Transform.position;
            dest = PlayerCamera.MouseHitPoint;

            exploreIndex = 0;
            dir = (dest - src);
            dir.y = 0f;
            dir.Normalize();
            _actualDest = src + dir * distance;
            time = distance / speed;
            _whites.Clear();

            for (var i = 0; i < exploreNum; ++i)
            {
                Timer.AddTask(time / 10 * i, InstanceExplore);
            }
        }
    }
}
