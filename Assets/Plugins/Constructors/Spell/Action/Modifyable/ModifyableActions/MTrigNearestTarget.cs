using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.GCamera;
using GameBase.Modify;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Action.Modifyables
{
    public class MTrigNearestTarget : ModifyableAction
    {
        private IModifieder _mOwner;
        private float _attackRange;
        private IProjectileTarget _target;
        private IProjectileOwner _pOwner;

        public TrigNearestTargetData data;



        protected override bool IsCast(Spell spell)
        {
            _mOwner = spell.speller as IModifieder;

            if (_mOwner == null)
            {
                return false;
            }

            _pOwner = spell.speller as IProjectileOwner;
            if (_pOwner == null)
            {
                return false;
            }

            if (!_mOwner.Modifyables.ContainsKey("attackRange"))
            {
                
                return false;
            }

            _attackRange = _mOwner.Modifyables["attackRange"];

            Vector3 center = new Vector3(spell.speller.Position.x, 0, spell.speller.Position.z);
            _target = TargetSetFactorary.GetTargetSet("Common").NearestTarget(center, _attackRange);
            if (_target == null)
            {
                return false;
            }

            return true;
        }

        protected override Flying GenFlying(Spell spell, float angleOffset, float flyingDistanceModify)
        {
            var e = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
            e.Src = _pOwner.HandPosition + new Vector3(0, 1, 0);
            Vector3 dir = (_target.Center - e.Src).normalized;
            Quaternion rotate = Quaternion.Euler(0, angleOffset, 0);
            e.curve.DirInit(rotate * dir);
            e.dest = _target.Center;

            return e;
        }

        protected override Projectile GenProjectile(Flying flying)
        {
            var e = Projectiles.Factory.Instance.Get(data.projectileType, data.projectileID);
            e.target = _target;
            e.Flying = flying;

            return e;
        }
    }

    public class MTrigNearestTargetCon : BaseConstructor<TrigNearestTargetData, MTrigNearestTarget, MTrigNearestTargetCon>
    {
        protected override string RelativePath => "Spell/Action/TrigNearestTarget.csv";

        protected override MTrigNearestTarget Get()
        {
            return new MTrigNearestTarget();
        }

        protected override void Parse(CsvReader line, ref TrigNearestTargetData data)
        {
            Enum.TryParse(line[1], out data.projectileType);
            data.projectileID = int.Parse(line[2]);
            Enum.TryParse(line[3], out data.flyingType);
            data.flyingID = int.Parse(line[4]);
        }

        protected override void Set(MTrigNearestTarget e, in TrigNearestTargetData data)
        {
            e.data = data;
        }
    }
}
