using GameBase.EntitySystem;
using GameBase.Indicators;
using GameBase.Tools;
using System.Collections.Generic;
using GameBase.GCamera;
using UnityEngine;

namespace GameBase.Spells
{
    public struct IndicatorConfig
    {
        public Vector3 position;
        public Vector3 targetPosition;
        public float length;
    }

    public class SpellConfig
    {
        public bool isIndicatorReady;
        public bool isFastCast;
        public IKeyInteractive interactive;
        public Spell spell;
    }

    public class SpellCaster : Singleton<SpellCaster>, IBaseSys
    {
        private List<KeyFunction> _spellKeys = new()
        {
            KeyFunction.Spell1,
            KeyFunction.Spell2,
            KeyFunction.Spell3,
            KeyFunction.Spell4,
            KeyFunction.Spell5,
            KeyFunction.Spell6,
            KeyFunction.Spell7,
        }; 
        

        private AutoFillList<SpellConfig> _spellConfigs = new();

        private IKeySpeller _speller; 

        public SpellCaster()
        {
            for (int i = 0; i < _spellKeys.Count; i++)
            {
                _spellConfigs.Add(new SpellConfig());
            }
            ShadowMono.CreateShadowMono(this);
        }

        private void DeReadyAll()
        {
            foreach (var config in _spellConfigs)
            {
                config.isIndicatorReady = false;
            }
        }

        public void SetActiverSpeller(IKeySpeller e)
        {
            _speller = e;
            for (int i = 0; i < _spellKeys.Count; ++i)
            {
                var spell = _speller.GetSpell(i);
                if (spell != null && spell.interactive is IKeyInteractive interactive)
                {
                    _spellConfigs[i].interactive = interactive;
                    _spellConfigs[i].spell = spell;
                }
            }
        }



        void IBaseSys.Update()
        {
            for (int i = 0; i < _spellKeys.Count; ++i)
            {
                var interactive = _spellConfigs[i].interactive;

                if (interactive == null)
                {
                    continue;
                }

                var spell = _spellConfigs[i].spell;

                if (!spell.spellCoolingdown.IsCoolingOver)
                {
                    continue;
                }

                var func = _spellKeys[i];
                var isFastCast = _spellConfigs[i].isFastCast;

                var indicator = Factory.Get(interactive.IndicatorType);

                if (isFastCast && Inputs.GetKeyDown(func, "spell"))
                {
                    interactive.Invoke();
                    DeReadyAll();
                }
                else if (!isFastCast)
                {
                    if (!_spellConfigs[i].isIndicatorReady && Inputs.GetKeyDown(func, "spell"))
                    {
                        _spellConfigs[i].isIndicatorReady = true;
                        indicator.Show();
                    }
                    else if (_spellConfigs[i].isIndicatorReady && Inputs.GetKeyDown(KeyFunction.MouseConfirm, "spell"))
                    {
                        interactive.Invoke();
                        _spellConfigs[i].isIndicatorReady = false;
                        indicator.Hide();
                    }
                    else if (Inputs.GetKeyDown(KeyFunction.Cancel, "spell"))
                    {
                        DeReadyAll();
                        indicator.Hide();
                    }
                }

                if (_spellConfigs[i].isIndicatorReady)
                {
                    indicator.Set(new IndicatorConfig()
                    {
                        length = interactive.Length,
                        position = _speller.Position,
                        targetPosition = CameraSys.MouseHitPosition
                    });
                }
            }
        }
        void IBaseSys.FixedUpdate()
        {

        }

        int IBaseSys.GetActiveCount()
        {
            return 0;
        }

        int IBaseSys.GetEntityCount()
        {
            return 0;
        }

        int IBaseSys.GetReleasedCount()
        {
            return 0;
        }
    }
}
