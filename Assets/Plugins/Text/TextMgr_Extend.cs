using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameBase.Texts
{
    public partial class TextMgr
    {
        private static string _buffTextFilePath = "BuffText.csv";
        private static string _spellActionModifierText = "SpellActionModifierText.csv";

        private static void ExtendInit()
        {
            Init(_buffTextFilePath);
            Init(_spellActionModifierText);
        }

        public static string GetBuffText(int id)
        {
            return Get(_buffTextFilePath, id);
        }

        public static string GetSpellActionModifierText(int id)
        {
            return Get(_spellActionModifierText, id);
        }
    }
}
