using GameBase.Tools;
using UnityEngine;

namespace GameBase.Modify
{
    public struct ModifierData
    {
        public int key;
        public float value;
        public ModifyType type1;
        public ModifyType type2;
    }
    public class ModifierDataBase : CsvDataBase<ModifierData, ModifierDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("ModifierDataBase.csv");
    }
}
