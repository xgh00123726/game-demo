using GameBase.Flyings;
using GameBase.Triggers;
using GameBase.Spells;
using GameBase.Tools;

namespace Constructor.Spells.Action
{
    public abstract partial class ModifyableAction : ISpellAction
    {
        private float _processedDisfuse = 0f;
        private float _angleInit = 0f;
        private float _angleDelta = 0f;

        public ModifyableAction()
        {
            _data.flyingDistance = 0;
            _data.flyingNums = 0;
            _data.fireDisfuse = 0;
            _data.castTimes = 0;
        }

        protected virtual bool IsCast(Spell spell)
        {
            return true;
        }

        protected virtual Flying GenFlying(Spell spell, float angleOffset, float flyingDistanceModify)
        {
            return null;
        }

        protected virtual Trigger GenProjectile(Flying flying)
        {
            return null;
        }

        protected virtual bool CastAction(Spell spell, in ModifyableModifyData modifyData)
        {
            if (!IsCast(spell))
            {
                return false;
            }

            var ret = false;

            for (int i = 0; i < 1 + _modifiedData.flyingNums; i++)
            {
                var flying = GenFlying(spell, GetAngleOffset(i), modifyData.flyingDistance);

                if (flying != null && spell.speller is ITriggerOwner pOwner) 
                {
                    var projectile = GenProjectile(flying);
                    if (projectile != null)
                    {
                        projectile.owner = pOwner;
                    }

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
