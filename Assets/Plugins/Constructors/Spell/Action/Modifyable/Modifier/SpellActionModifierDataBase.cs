using NReco.Csv;
using System;
using System.IO;
using UnityEngine;
using GameBase.Tools;

namespace Constructor.Spells.Action.Modifyables.Modifier
{
    public struct SpellActionModifierInfo
    {
        public Type type;
        public int id;
        public int iconTextureID;
        public int rarity;
    }

    public class SpellActionModifierDataBase
    {
        private static SpellActionModifierInfo[] _infos;
        
        static SpellActionModifierDataBase()
        {
            _infos = new CsvReaderReflect<SpellActionModifierInfo>()
                .Parse($"{Application.streamingAssetsPath}/ConstructorData/Spell/Action/Modifyable/Modifier/SpellActionModifierInfo.csv");
        }

        public static SpellActionModifierInfo GetInfo(int id)
        {
            return _infos[id];
        }
    }
}
