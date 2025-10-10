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

    public class FireDisfuseModifierCon : BaseConstructor<FireDisfuseModifierData, FireDisfuseModifier, FireDisfuseModifierCon>
    {
        protected override string RelativePath => "Spell/Action/Modifyable/Modifier/FireDisfuseModifier.csv";

        protected override FireDisfuseModifier Get()
        {
            return new FireDisfuseModifier(0);
        }
        protected override void Set(FireDisfuseModifier e, in FireDisfuseModifierData data)
        {
            e.data = data;
        }
    }
}
