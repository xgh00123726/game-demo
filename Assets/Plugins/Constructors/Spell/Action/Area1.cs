using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.GCamera;
using GameBase.Projectiles;
using GameBase.Spells;
using NReco.Csv;
using System;
using UnityEditor.Experimental.GraphView;

namespace Constructor.Spells.Action
{
    public struct Area1Data
    {
        public int flyingID;
        public int projectileID;
    }

    public class Area1Action : IAction
    {
        public int flyingID;
        public int projectileID;

        public void CastAction(Spell spell)
        {
            if (spell.speller is IProjectileOwner pOwner)
            {
                var ef = Constructor.Flyings.Common.Instance.Get(flyingID);
                ef.Src = pOwner.HandPosition + new UnityEngine.Vector3(0, 10, 0);
                ef.dest = CameraSys.MouseHitPosition;
                ef.OnReleased = () =>
                {
                    var e = Constructor.Projectiles.Area1.Instance.Get(projectileID);
                    e.flying.Src = ef.dest;
                    e.flying.dest = ef.dest;
                    e.owner = pOwner;
                };
            }
        }
    }
    public class Area1 : BaseConstructor<Area1Data, Area1Action, Area1>
    {
        protected override string RelativePath => "Spell/Action/Area1.csv";

        protected override Area1Action Get(Action<Area1Action> Init)
        {
            var e = new Area1Action();
            Init(e);
            return e;
        }

        protected override void Parse(CsvReader line, ref Area1Data data)
        {
            data.flyingID = int.Parse(line[1]);
            data.projectileID = int.Parse(line[2]);
        }

        protected override void Set(Area1Action e, in Area1Data data)
        {
            e.flyingID = data.flyingID;
            e.projectileID = data.projectileID;
        }
    }
}
