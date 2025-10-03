using GameBase.Tools;
using UnityEngine;

namespace GameBase.Modify
{
    public struct ModifyableData
    {
        public int id;
        public string name;
        public int iconTextureID;
    }
    public class ModifyableDataBase : CsvDataBase<ModifyableData, ModifyableDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("ModifyableDataBase.csv");
    }
}
