using NReco.Csv;
using System;
using System.IO;
using UnityEngine;
using GameBase.Tools;

namespace Constructor.Spells.Action
{
    public struct SpellActionModifierInfo
    {
        public ModifierType type;
        public int id;
        public int iconTextureID;
        public int rarity;
    }

    public class SpellActionModifierDataBase : CsvDataBase<SpellActionModifierInfo,  SpellActionModifierDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("SpellActionModifierInfo.csv");
    }
}
