using GameBase.EntitySystem;

namespace Constructor.Spells.Action
{
    public struct FireDisfuseModifierData
    {
        public float fireDisfuseModify;
    }
    public class FireDisfuseModifier : BaseModifier
    {
        public FireDisfuseModifierData data;
        public FireDisfuseModifier(float fireDisfuseModify)
        {
            data.fireDisfuseModify = fireDisfuseModify;
        }

        internal override void Modify(ref ModifyableModifyData modifyData)
        {
            modifyData.fireDisfuse += data.fireDisfuseModify;
        }
    }

    public class FireDisfuseModifierCon : SealedConstructor<FireDisfuseModifierData, FireDisfuseModifier, FireDisfuseModifierCon>
    {
        protected override string RelativePath => "Spell/Action/Modifyable/Modifier/FireDisfuseModifier.csv";

        protected override FireDisfuseModifier GetFromData(in FireDisfuseModifierData data)
        {
            var e = new FireDisfuseModifier(0);
            e.data = data;
            return e;
        }
    }
}
