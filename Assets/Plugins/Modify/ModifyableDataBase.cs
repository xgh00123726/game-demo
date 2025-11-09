using GameBase.Tools;
using UnityEngine;

namespace GameBase.Modify
{
    public struct ModifyableData
    {
        public int id;
        public string name;
        public string textureName;
    }
    public class ModifyableDataBase : CsvDataBase<ModifyableData, ModifyableDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("ModifyableDataBase.csv");
    }
}
