using Constructor.Spells.Action.Modifyables;
using GameBase.Infos;
using GameBase.Spells;
using GameBase.Tools;
using Instance;
using Instance.UI.MVC;
namespace GameBase.UI
{
    public class SpellActionModifierViewPanel : BaseViewPanel<SpellActionModifierViewItem>
    {
        private static SpellActionModifierViewPanel _instance = new();
        public static SpellActionModifierViewPanel Instance => _instance;

        private SpellShadowView _spellShadowView;

        public SpellActionModifierViewPanel(int prefabID = 43,
            int defaultObjID = 42) : base(
            prefabID,
            defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }

            _spellShadowView = new SpellShadowView(panel.transform);
        }

        public void UpdateSpell(Spell spell)
        {
            if (spell.action is ModifyableAction mAct)
            {
                _spellShadowView.image.SetIcon(spell.iconTextureID);
                _spellShadowView.image.SetColor(UnityEngine.Color.green);
                _spellShadowView.image.Show();
                FillItem(mAct.Size);

                for (int i = 0; i < mAct.Size; i++)
                {
                    if (mAct.HasItem(i))
                    {
                        var inventoryID = mAct.GetData(i).inventoryID;
                        var inventoryInfo = CommonInventoryDataBase.Get(inventoryID);
                        //this[i].triggerImage.SetIcon(inventoryInfo.iconTextureID);
                        //this[i].triggerImage.SetColor(inventoryInfo.rarity);
                        this[i].triggerImage.Show();
                    }
                    else
                    {
                        this[i].triggerImage.HideColor();
                        this[i].triggerImage.Hide();
                    }
                }
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}
