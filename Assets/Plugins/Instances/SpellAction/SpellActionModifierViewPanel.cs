using Constructor.Spells;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.UI;
namespace Instance
{
    public class SpellActionModifierViewPanel : BaseViewPanel<SpellActionModifierViewItem>
    {
        private static SpellActionModifierViewPanel _instance;
        public static SpellActionModifierViewPanel Instance => _instance;

        private SpellShadowView _spellShadowView;

        public SpellActionModifierViewPanel(string prefabName = "Prefabs/UI/SpellActionModifierPanel.prefab",
            string itemPrefabName = "Prefabs/UI/SpellActionModifierItem.prefab") : base(
            prefabName,
            itemPrefabName)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }
            _instance = this;
            _spellShadowView = new SpellShadowView(panel.transform);
        }

        public void UpdatePanel(Spell spell)
        {
            if (spell.Action is ModifyableAction mAct)
            {
                _spellShadowView.Image.SetIcon(spell.TextureName);
                _spellShadowView.Image.SetColor(UnityEngine.Color.green);
                _spellShadowView.Image.Show();
                FillItem(mAct.Size);

                for (int i = 0; i < mAct.Size; i++)
                {
                    this[i].Obj.SetActive(true);
                    int id = mAct.GetID(i);
                    if (id >= 0)
                    {
                        var data = SpellActionModifierDataBase.Instance[id];

                        this[i].TriggerImage.SetIcon(data.textureName);
                        this[i].TriggerImage.SetColor(data.rarity);
                        this[i].TriggerImage.Show();
                    }
                    else
                    {
                        this[i].TriggerImage.HideColor();
                        this[i].TriggerImage.Hide();
                    }
                }
                for (int i = mAct.Size; i < Entities.Count; i++)
                {
                    this[i].Obj.SetActive(false);
                }
            }
        }
    }
}
