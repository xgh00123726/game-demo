using GameBase.Modify;
using System.Collections.Generic;

namespace GameBase.Creatures
{
    public partial class Creature
    {
        public Modifyables modifyables = new();
        private Dictionary<string, float> _possibleAttr = new();

        public static float _agiToMoveSpeedPercentFactor = 0.2f;
        public static float _agiToAttackSpeedFactor = 5f;

        public static float _strgToMaxHPFactor = 10f;
        public static float _strgToHealthRegenFactor = 0.1f;

        public static float _intlToMaxManaFactor = 5f;
        public static float _intlToCooldownFactor = 5f;

        public static float _uniToDamageFactor = 1f;
        public static float _uniToAgiPercentFactor = 0.01f;
        public static float _uniToStrgPercentFactor = 0.01f;
        public static float _uniToIntlPercentFactor = 0.01f;

        private Modifyer _agiToMoveSpeedPercent;
        private Modifyer _agiToAttackSpeed;

        private Modifyer _strgToMaxHP;
        private Modifyer _strgToHealthRegen;

        private Modifyer _intlToMaxMana;
        private Modifyer _intlToCooldown;

        private Modifyer _uniToDamage;
        private Modifyer _uniToAgiPercent;
        private Modifyer _uniToStrgPercent;
        private Modifyer _uniToIntlPercent;

        internal void HighLevelAttrInit()
        {
            _agiToMoveSpeedPercent = ModifyerSys.Instance.NewEntity();
            _agiToAttackSpeed      = ModifyerSys.Instance.NewEntity();

            _strgToMaxHP           = ModifyerSys.Instance.NewEntity();
            _strgToHealthRegen     = ModifyerSys.Instance.NewEntity();

            _intlToMaxMana         = ModifyerSys.Instance.NewEntity();
            _intlToCooldown        = ModifyerSys.Instance.NewEntity();

            _uniToDamage           = ModifyerSys.Instance.NewEntity();
            _uniToAgiPercent       = ModifyerSys.Instance.NewEntity();
            _uniToStrgPercent      = ModifyerSys.Instance.NewEntity();
            _uniToIntlPercent      = ModifyerSys.Instance.NewEntity();

            _agiToMoveSpeedPercent.value = 0;
            _agiToMoveSpeedPercent.type  = ModifyType.Aways | ModifyType.Temporary;
            _agiToMoveSpeedPercent.AddTo(modifyables["moveSpeed"]);

            _agiToAttackSpeed.value      = 0;
            _agiToAttackSpeed.type       = ModifyType.Aways | ModifyType.Temporary;
            _agiToAttackSpeed.AddTo(modifyables["attackSpeed"]);

            _strgToMaxHP.value           = 0;
            _strgToMaxHP.type            = ModifyType.Aways | ModifyType.Temporary;
            _strgToMaxHP.AddTo(modifyables["maxHP"]);
            _strgToHealthRegen.value     = 0;
            _strgToHealthRegen.type      = ModifyType.Aways | ModifyType.Temporary;
            _strgToHealthRegen.AddTo(modifyables["healthRegen"]);

            _intlToMaxMana.value         = 0;
            _intlToMaxMana.type          = ModifyType.Aways | ModifyType.Temporary;
            _intlToMaxMana.AddTo(modifyables["maxMana"]);
            _intlToCooldown.value        = 0;
            _intlToCooldown.type         = ModifyType.Aways | ModifyType.Temporary;
            _intlToCooldown.AddTo(modifyables["coolingAccelerate"]);

            _uniToDamage.value           = 0;
            _uniToDamage.type            = ModifyType.Aways | ModifyType.Temporary;
            _uniToDamage.AddTo(modifyables["damage"]);
            _uniToAgiPercent.value       = 0;
            _uniToAgiPercent.type        = ModifyType.Aways | ModifyType.Temporary;
            _uniToAgiPercent.AddTo(modifyables["agility"]);
            _uniToStrgPercent.value      = 0;
            _uniToStrgPercent.type       = ModifyType.Aways | ModifyType.Temporary;
            _uniToStrgPercent.AddTo(modifyables["strength"]);
            _uniToIntlPercent.value      = 0;
            _uniToIntlPercent.type       = ModifyType.Aways | ModifyType.Temporary;
            _uniToIntlPercent.AddTo(modifyables["intelligence"]);
        }

        internal void HighLevelAttrUpdate()
        {
            float agi = modifyables["agility"].Value;
            float strg = modifyables["strength"].Value;
            float intl = modifyables["intelligence"].Value;
            float uni = modifyables["universal"].Value;


            _agiToMoveSpeedPercent.value = agi * _agiToMoveSpeedPercentFactor;
            _agiToAttackSpeed.value = agi * _agiToAttackSpeedFactor;

            _strgToMaxHP.value = strg * _strgToMaxHPFactor;
            _strgToHealthRegen.value = strg * _strgToHealthRegenFactor;

            _intlToMaxMana.value = intl * _intlToMaxManaFactor;
            _intlToCooldown.value = intl * _intlToCooldownFactor;

            _uniToDamage.value = uni * _uniToDamageFactor;
            _uniToAgiPercent.value = uni * _uniToAgiPercentFactor;
            _uniToStrgPercent.value = uni * _uniToStrgPercentFactor;
            _uniToIntlPercent.value = uni * _uniToIntlPercentFactor;
        }

        internal void HighLevelAttrDispose()
        {
            ModifyerSys.Instance.RemoveEntity(_agiToMoveSpeedPercent);
            ModifyerSys.Instance.RemoveEntity(_agiToAttackSpeed);

            ModifyerSys.Instance.RemoveEntity(_strgToMaxHP);
            ModifyerSys.Instance.RemoveEntity(_strgToHealthRegen);

            ModifyerSys.Instance.RemoveEntity(_intlToMaxMana);
            ModifyerSys.Instance.RemoveEntity(_intlToCooldown);

            ModifyerSys.Instance.RemoveEntity(_uniToDamage);
            ModifyerSys.Instance.RemoveEntity(_uniToAgiPercent);
            ModifyerSys.Instance.RemoveEntity(_uniToStrgPercent);
            ModifyerSys.Instance.RemoveEntity(_uniToIntlPercent);
        }

        public bool HasPossibleAttr(string name)
        {
            return _possibleAttr.ContainsKey(name);
        }

        public void AddPossibleAttr(string name)
        {
            _possibleAttr.Add(name, default);
        }

        public void SetPossibleAttr(string name, float value)
        {
            _possibleAttr[name] = value;
        }

        public float GetPossibleAttr(string name)
        {
            return _possibleAttr[name];
        }
    }
}
