using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Modify;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public struct TrigNearestTargetData
    {
        public Projectiles.Type projectileType;
        public int projectileID;
        public Flyings.Type flyingType;
        public int flyingID;
    }

    public class TrigNearestTargetAction : IAction
    {
        public TrigNearestTargetData data;
        void IAction.CastAction(Spell spell)
        {
            var mOwner = spell.speller as IModifyOwner<float>;
            if (mOwner == null)
            {
                XLogger.Instance.Log("owner null");
                return;
            }
            if (!mOwner.Modifyables.ContainsValueWith("attackRange"))
            {
                XLogger.Instance.Log("no attack range");
                return;
            }
            float attackRange = mOwner.Modifyables["attackRange"].Value;
            Vector3 center = new Vector3(spell.speller.Position.x, 0, spell.speller.Position.z);

            var target = TargetSetFactorary.GetTargetSet("Common").NearestTarget(center, attackRange);
            if (target == null)
            {
                XLogger.Instance.Log($"pos:{center}, no target");
                return;
            }

            var pOwner = spell.speller as IProjectileOwner;

            var e = Projectiles.Factory.Instance.Get(data.projectileType, data.projectileID);
            e.flying = Flyings.Factory.Instance.Get(data.flyingType, data.flyingID);
            e.flying.Src = pOwner.HandPosition + new Vector3(0, 1, 0);
            e.flying.dest = CameraSys.MouseHitPosition;
            e.owner = pOwner;
            e.target = target;
        }
    }

    public class TrigNearestTarget : BaseConstructor<TrigNearestTargetData, TrigNearestTargetAction, TrigNearestTarget>
    {
        protected override string RelativePath => "Spell/Action/TrigNearestTarget.csv";

        protected override TrigNearestTargetAction Get(Action<TrigNearestTargetAction> Init)
        {
            var e = new TrigNearestTargetAction();
            Init(e);
            return e;
        }

        protected override void Parse(CsvReader line, ref TrigNearestTargetData data)
        {
            Enum.TryParse(line[1], out data.projectileType);
            data.projectileID = int.Parse(line[2]);
            Enum.TryParse(line[3], out data.flyingType);
            data.flyingID = int.Parse(line[4]);
        }

        protected override void Set(TrigNearestTargetAction e, in TrigNearestTargetData data)
        {
            e.data = data;
        }
    }
}
