using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Indicators;
using GameBase.Tools;
using System.Collections.Generic;
using Constructor.Spells.Interactive;
using GameBase.Spells;

namespace Instance
{
    public class PlayerSpellCaster : SingletonInstance<PlayerSpellCaster>
    {
        private static Creature _target;

        private static List<KeyFunction> _spellKeys = new()
        {
            KeyFunction.Spell1,
            KeyFunction.Spell2,
            KeyFunction.Spell3,
            KeyFunction.Spell4,
            KeyFunction.Spell5,
            KeyFunction.Spell6,
            KeyFunction.Spell7,
        };

        public static void SetHotKey(int position, KeyFunction key)
        {
            _spellKeys[position] = key;
        }

        private static int _spellsNum = 0;
        private static AutoFillList<bool> _isIndicatorReadys = new();
        private static AutoFillList<bool> _isFastCasts = new();

        private static void DeReadyAll()
        {
            for (int i = 0; i < _isIndicatorReadys.Count; i++) 
            {
                _isIndicatorReadys[i] = false;
            }
        }

        public static void SetTarget(Creature c)
        {
            _target = c;
            _spellsNum = c.Spells.Size;
            _isIndicatorReadys.Resize(_spellsNum, false);
            _isFastCasts.Resize(_spellsNum, false);
        }

        protected override void Update()
        {
            for (int i = 0; i < _spellsNum; ++i)
            {
                var spell = _target.Spells[i];

                var interactive = spell.interactive as Invokable;

                if (interactive == null)
                {
                    return;
                }

                if (!spell.spellCoolingdown.IsCoolingOver)
                {
                    continue;
                }

                var func = _spellKeys[i];
                var isFastCast = _isFastCasts[i];
                var isIndicatorReady = _isIndicatorReadys[i];

                var indicator = CastIndicatorFactory.Get(interactive.indicatorType);

                if (isFastCast && Inputs.GetKeyDown(func, "spell"))
                {
                    spell.TryCast();
                    DeReadyAll();
                }
                else if (!isFastCast)
                {
                    if (!isIndicatorReady && Inputs.GetKeyDown(func, "spell"))
                    {
                        _isIndicatorReadys[i] = true;
                        indicator.Show();
                    }
                    else if (isIndicatorReady && Inputs.GetKeyDown(KeyFunction.MouseConfirm, "spell"))
                    {
                        spell.TryCast();

                        _isIndicatorReadys[i] = false;
                        indicator.Hide();
                    }
                    else if (Inputs.GetKeyDown(KeyFunction.Cancel, "spell"))
                    {
                        DeReadyAll();
                        indicator.Hide();
                    }
                }

                isIndicatorReady = _isIndicatorReadys[i];

                if (isIndicatorReady)
                {
                    indicator.Set(new IndicatorConfig()
                    {
                        length = interactive.indicatorLength,
                        position = _target.Position,
                        targetPosition = CameraSys.MouseHitPosition
                    });
                }
            }
        }
    }
}
