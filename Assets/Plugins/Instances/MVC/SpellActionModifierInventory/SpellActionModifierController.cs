using Constructor.Spells.Action.Modifyables;
using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameBase.UI.MVC;
using GameBase.Creatures;
namespace Instance.UI.MVC
{
    public class SpellActionModifierController : InventoryController<InventoryData, 
        SpellActionModifierViewItem, 
        SpellActionModifierViewPanel, 
        SpellActionModifierController>
    {
        private List<SpellActionModifierInventoryModel> _models = new()
        {
            new() {Size = 5},
            new() {Size = 5},
            new() {Size = 5},
            new() {Size = 5},
        };

        private SpellActionModifierInventoryModel _model;

        public SpellActionModifierController()
        {
            View.FillItem(5);
            View.DragableControl = new SpellActionModifierDragableControl();
            Hide();
            _model = _models[0];
            SpellController.Instance.OnClickedItem += (int index) =>
            {
                _model = _models[index];
                SpellShadowView.CopyIcon(SpellController.Instance.GetViewItem(index));
                ForceRefreshView();
                Show();
            };
        }

        protected override IMVCModel<InventoryData> Model => _model;

        protected override SpellActionModifierViewPanel View => SpellActionModifierViewPanel.Instance;

        public override void Show()
        {
            base.Show();

            if (_model.owner.Spell.action is ModifyableAction mAct)
            {
                SpellShadowView.ShowColor(Color.green);
            }
            else
            {
                SpellShadowView.RestoreColor();
            }

            SpellShadowView.Show();
        }

        public override void Hide()
        {
            base.Hide();
            SpellShadowView.Hide();
        }

        public void SetOwner(Creature owner)
        {
            for (int i = 0; i < _models.Count; ++i)
            {
                _models[i].owner = new SpellActionModifierOwner(owner, i);
            }
        }
    }
}
