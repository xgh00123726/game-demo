using GameBase.UI;
using GameBase.UI.MVC;
namespace Instance.UI.MVC
{
    public class SpellActionModifierDetailControl : IDetailableControl<SpellActionModifierViewItem>
    {
        bool IDetailableControl<SpellActionModifierViewItem>.IsDetail(SpellActionModifierViewItem e)
        {
            return e.Obj.EnterTime > 0.2f;
        }

        void IDetailableControl<SpellActionModifierViewItem>.OnDetail(SpellActionModifierViewItem e)
        {
            
        }

        void IDetailableControl<SpellActionModifierViewItem>.OnEnterDetail(SpellActionModifierViewItem e)
        {
            
        }

        void IDetailableControl<SpellActionModifierViewItem>.OnExitDetail(SpellActionModifierViewItem e)
        {
            
        }
    }
}
