using Constructor.Spells.Action.Modifyables.Modifier;
using GameBase.Flyings;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.Tools;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Constructor.Spells.Action.Modifyables
{
    public abstract class ModifyableAction : ISpellAction
    {
        private ModifyableModifyData _data;
        private ModifyableModifyData _modifiedData;
        private AutoFillList<BaseModifier> _modifiers = new();

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

        protected virtual Projectile GenProjectile(Flying flying)
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

                if (flying != null && spell.speller is IProjectileOwner pOwner) 
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



        public void AddModifier(BaseModifier modifier, int index)
        {
            _modifiers.Add(modifier, index);
            ResolveModifiedData();
        }

        public void RemoveModifyer(int index)
        {
            if (index < 0 || index >= _modifiers.Count)
            {
                XLogger.Instance.Log($"invalid index:{index}, max:{_modifiers.Count}");
                return;
            }
            _modifiers[index] = null;
            ResolveModifiedData();
        }

        public static void TryRemoveModifyer(Spell spell, int index)
        {
            if (spell.action is ModifyableAction mAct)
            {
                mAct.RemoveModifyer(index);
            }
        }

        public void ResolveModifiedData()
        {
            _modifiedData = _data;
            foreach (var modifier in _modifiers)
            {
                if (modifier == null)
                {
                    continue;
                }

                modifier.Modify(ref _modifiedData);
            }

            if (_modifiedData.flyingNums < 0)
            {
                _modifiedData.flyingNums = 0;
            }

            _processedDisfuse = ProcessDisfuse(_modifiedData.fireDisfuse);
            if (_modifiedData.flyingNums > 0)
            {
                _angleInit = -_processedDisfuse / 2;
                _angleDelta = _processedDisfuse / _modifiedData.flyingNums;
            }
        }
    }
}
