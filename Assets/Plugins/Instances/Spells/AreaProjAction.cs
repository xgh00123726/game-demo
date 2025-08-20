using GameBase.Projectiles;
using GameBase.GCamera;
using GameBase.Spells;

namespace Instance.Spells
{
    public class AreaProjAction : IAction
    {
        public AreaProjAction(int flyingID = 1, int projectileID = 1)
        {
            this.flyingID = flyingID;
            this.projectileID = projectileID;
        }
        public int flyingID;
        public int projectileID;
        void IAction.CastAction(Spell spell)
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
}
