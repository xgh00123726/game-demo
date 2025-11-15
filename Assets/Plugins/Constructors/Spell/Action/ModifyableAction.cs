using Constructor.Triggers;
using GameBase.Flyings;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;

namespace Constructor.Spells
{
    public abstract partial class ModifyableAction : ISpellAction
    {

        private float _processedDisfuse = 0f;
        private float _angleInit = 0f;
        private float _angleDelta = 0f;

        protected virtual Projectile GenProjectile(Spell spell, in SpellActionModifierData modifyData)
        {
            return null;
        }

        protected virtual void RecorrectFlying(Flying flying) { }

        protected virtual bool CastAction(Spell spell, in SpellActionModifierData modifyData)
        {
            var ret = false;

            for (int i = 0; i < 1 + _modifiedData.flyingNums; i++)
            {
                var projectile = GenProjectile(spell, modifyData);

                if (projectile != null && projectile.flying != null) 
                {
                    projectile.flying.startAngleOffset = GetAngleOffset(i);
                    RecorrectFlying(projectile.flying);
                    ret = true;
                }
            }

            return ret;
        }

        protected float Disfuse => _processedDisfuse;

        protected float AngleInit => _angleInit;

        protected float AngleDelta => _angleDelta;

        bool ISpellAction.CastAction(Spell spell)
        {
            var ret = false;
            ret |= CastAction(spell, in _modifiedData);
            if (_modifiedData.castTimes > 0)
            {
                for (int i = 1; i <= _modifiedData.castTimes; ++i)
                {
                    Timer.AddTask(i * 0.2f, () =>
                    {
                        CastAction(spell, in _modifiedData);
                    });
                }
            }

            return ret;
        }

        private float ProcessDisfuse(float origin)
        {
            const float MIN_DISFUSE = 30f;
            if (origin < 0)
            {
                return MIN_DISFUSE * MIN_DISFUSE / (-origin + MIN_DISFUSE);
            }
            else
            {
                return origin;
            }
        }

        protected float GetAngleOffset(int index)
        {
            return _angleInit + _angleDelta * index;
        }
    }
}
