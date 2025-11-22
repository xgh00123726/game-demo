using GameBase.EntitySystem;
using GameBase.Modify;
using UnityEngine;

namespace Constructor.AttrAmplify
{
    [GenTemplate]
    public class AttrAmplifierCsvFactory : CsvFactory<AttrAmplifierData, Modifyer, AttrAmplifierCsvFactory>
    {
        protected override string Folder => $"{Application.streamingAssetsPath}/AttrAmplifier";

        protected override Modifyer GetEntity(AttrAmplifierData data)
        {

            return null;
        }
    }
}
