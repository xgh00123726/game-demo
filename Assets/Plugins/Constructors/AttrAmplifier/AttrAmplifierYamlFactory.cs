using GameBase.EntitySystem;
using GameBase.Modify;
using UnityEngine;

namespace Constructor.AttrAmplify
{
    public class AttrAmplifierYamlFactory : YamlFactory<AttrAmplifierData, Modifyer, AttrAmplifierYamlFactory>
    {
        protected override string Folder => $"{Application.streamingAssetsPath}/AttrAmplifier";

        protected override Modifyer GetEntity(AttrAmplifierData data)
        {
            return null;
        }
    }
}
