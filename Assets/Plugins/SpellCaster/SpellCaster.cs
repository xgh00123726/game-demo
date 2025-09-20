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
            for (int i = 0; i < _spellConfigs.Count; i++)
            {
                _spellConfigs.Add(new SpellConfig());
            }
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

                var func = _spellKeys[i];
                var isFastCast = _spellConfigs[i].isFastCast;

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
                    }
                    else if (_spellConfigs[i].isIndicatorReady && Inputs.GetKeyDown(KeyFunction.MouseConfirm, "spell"))
                    {
                        interactive.Invoke();
                        _spellConfigs[i].isIndicatorReady = false;
                    }
                    else if (Inputs.GetKeyDown(KeyFunction.Cancel, "spell"))
                    {
                        DeReadyAll();
                    }
                }

                if (_spellConfigs[i].isIndicatorReady)
                {
                    Factory.Get(interactive.IndicatorType).Set(new IndicatorConfig()
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
