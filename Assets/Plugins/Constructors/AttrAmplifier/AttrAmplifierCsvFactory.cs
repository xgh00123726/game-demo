using GameBase.EntitySystem;
using GameBase.Modify;
using UnityEngine;

namespace Constructor.AttrAmplify
{
    [GenTemplate]
    public class AttrAmplifierCsvFactory : CsvFactory<AttrAmplifierData, AttrAmplifier, AttrAmplifierCsvFactory>
    {
        protected override string Folder => $"{Application.streamingAssetsPath}/AttrAmplifier";

        protected override void OnInitCsvData(ref AttrAmplifierData data)
        {
            data.IntKey = ModifyTable.GetID(data.Key);
        }
        protected override AttrAmplifier GetEntity(AttrAmplifierData data)
        {
            var amp = new AttrAmplifier();
            var m = ModifyerSys.Instance.NewEntity();
            m.Type = ModifyType.Forever | ModifyType.Always;
            m.Value = Random.Range(data.ValueMin, data.ValueMax);
            amp.Modifier = m;
            amp.Name = data.Name;
            amp.TextureName = data.TextureName;
            amp.Rarity = data.Rarity;
            amp.ID = data.ID;
            return amp;
        }
    }
}
