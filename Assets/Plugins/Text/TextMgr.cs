using GameBase.Tools;
using NReco.Csv;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Texts
{
    internal enum ParseState
    {
        Normal,
        MeetPlaceHolder,
        MeetKeywordsFirstK,
        MeetKeywordsKDot,
        ParsingKeywordStr,
    }

    public static class TextMgr
    {
        internal static string lang;
        public static string Lang
        {
            get => lang;
            set
            {
                if (lang == null || (lang != null && lang != value))
                {
                    var properties = typeof(TextMgr).GetProperties();
                    foreach (var property in properties)
                    {
                        property.PropertyType.GetProperty("Lang")?.SetValue(
                            property.GetValue(null), value);
                    }
                    if (lang == null)
                    {
                        LoadAll();
                    }

                    lang = value;
                }
            }
        }

        public static void LoadAll()
        {
            var properties = typeof(TextMgr).GetProperties();
            foreach (var property in properties)
            {
                property.PropertyType.GetMethod("LoadData")?.Invoke(property.GetValue(null), new object[]
                {
                    $"{property.Name}.yaml"
                });
            }
        }
        public static KeywordsMgr Keywords { get; } = KeywordsMgr.Instance;
        public static YamlTextLoader<EquipmentTextData> Equipment { get; } = new();
        public static YamlTextLoader<SpellActionTextData> SpellAction { get; } = new();
        public static YamlTextLoader<ModifierTextData> Modifier {  get; } = new();
    }
}
