using GameBase.Modify;
using System.Collections.Generic;

namespace GameBase.Creatures
{
    public partial class Creature :
        IModifieder
    {
        private Modifyables _modifyables = new();
        private Dictionary<string, float> _possibleAttr = new();

        public static float _agiToMoveSpeedFactor = 0.01f;
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

        public Modifyables Modifyables => _modifyables;

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

            _agiToMoveSpeedPercent.Value = 0;
            _agiToMoveSpeedPercent.Type  = ModifyType.Always | ModifyType.Temporary;
            _agiToMoveSpeedPercent.AddTo(_modifyables["moveSpeed"]);

            _agiToAttackSpeed.Value      = 0;
            _agiToAttackSpeed.Type       = ModifyType.Always | ModifyType.Temporary;
            _agiToAttackSpeed.AddTo(_modifyables["attackSpeed"]);

            _strgToMaxHP.Value           = 0;
            _strgToMaxHP.Type            = ModifyType.Always | ModifyType.Temporary;
            _strgToMaxHP.AddTo(_modifyables["maxHP"]);
            _strgToHealthRegen.Value     = 0;
            _strgToHealthRegen.Type      = ModifyType.Always | ModifyType.Temporary;
            _strgToHealthRegen.AddTo(_modifyables["healthRegen"]);

            _intlToMaxMana.Value         = 0;
            _intlToMaxMana.Type          = ModifyType.Always | ModifyType.Temporary;
            _intlToMaxMana.AddTo(_modifyables["maxMana"]);
            _intlToCooldown.Value        = 0;
            _intlToCooldown.Type         = ModifyType.Always | ModifyType.Temporary;
            _intlToCooldown.AddTo(_modifyables["coolingAccelerate"]);

            _uniToDamage.Value           = 0;
            _uniToDamage.Type            = ModifyType.Always | ModifyType.Temporary;
            _uniToDamage.AddTo(_modifyables["damage"]);
            _uniToAgiPercent.Value       = 0;
            _uniToAgiPercent.Type        = ModifyType.Always | ModifyType.Temporary;
            _uniToAgiPercent.AddTo(_modifyables["agility"]);
            _uniToStrgPercent.Value      = 0;
            _uniToStrgPercent.Type       = ModifyType.Always | ModifyType.Temporary;
            _uniToStrgPercent.AddTo(_modifyables["strength"]);
            _uniToIntlPercent.Value      = 0;
            _uniToIntlPercent.Type       = ModifyType.Always | ModifyType.Temporary;
            _uniToIntlPercent.AddTo(_modifyables["intelligence"]);
        }

        internal void HighLevelAttrUpdate()
        {
            float agi = _modifyables["agility"].Value;
            float strg = _modifyables["strength"].Value;
            float intl = _modifyables["intelligence"].Value;
            float uni = _modifyables["universal"].Value;


            _agiToMoveSpeedPercent.Value = agi * _agiToMoveSpeedFactor;
            _agiToAttackSpeed.Value = agi * _agiToAttackSpeedFactor;

            _strgToMaxHP.Value = strg * _strgToMaxHPFactor;
            _strgToHealthRegen.Value = strg * _strgToHealthRegenFactor;

            _intlToMaxMana.Value = intl * _intlToMaxManaFactor;
            _intlToCooldown.Value = intl * _intlToCooldownFactor;

            _uniToDamage.Value = uni * _uniToDamageFactor;
            _uniToAgiPercent.Value = uni * _uniToAgiPercentFactor;
            _uniToStrgPercent.Value = uni * _uniToStrgPercentFactor;
            _uniToIntlPercent.Value = uni * _uniToIntlPercentFactor;
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
