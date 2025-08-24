using GameBase.Indicators;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;

namespace Constructor.Spells.Interactive
{
    public struct CommonData
    {
        public ISpellIndicator.Type indicatorType;
        public float radius;
        public float length;
    }

    public class KeyInteractive : IInteractive
    {
        public KeyFunction readyKey;
        public KeyFunction castKey;
        public KeyFunction cancelKey;
        public ISpellIndicator indicator;
        public bool fastCast;

        bool IInteractive.ReadyTrig => fastCast ? true : Inputs.GetKeyDown(readyKey);

        bool IInteractive.CastTrig => fastCast ? Inputs.GetKeyDown(readyKey) : Inputs.GetKeyDown(castKey);

        bool IInteractive.CancelTrig => Inputs.GetKeyDown(cancelKey);

        void IInteractive.OnCancel()
        {
            indicator?.Hide();
        }

        void IInteractive.OnCast()
        {
            indicator?.Hide();
        }

        void IInteractive.OnReady()
        {
            indicator?.Show();
        }

        void IInteractive.OnReadying(ISpeller speller)
        {
            indicator?.Update(speller, GameBase.GCamera.CameraSys.MouseHitPosition);
        }
    }

    public class KeyCommon : BaseConstructor<CommonData, KeyInteractive, KeyCommon>
    {
        protected override string RelativePath => "Spell/Interactive/KeyCommon.csv";

        protected override KeyInteractive Get(Action<KeyInteractive> Init)
        {
            var e = new KeyInteractive();
            Init(e);
            return e;
        }

        protected override void Parse(CsvReader line, ref CommonData data)
        {
            Enum.TryParse(line[1], out data.indicatorType);
            data.radius = float.Parse(line[2]);
            data.length = float.Parse(line[3]);
        }

        protected override void Set(KeyInteractive e, in CommonData data)
        {
            e.cancelKey = KeyFunction.Cancel;
            e.castKey = KeyFunction.MouseConfirm;


            switch (data.indicatorType)
            {
                default:
                case ISpellIndicator.Type.Circle:
                    e.indicator = IndicatorSys.Instance.NewEntity<CircleIndicator>();
                    break;
                case ISpellIndicator.Type.Linear:
                    e.indicator = IndicatorSys.Instance.NewEntity<LinearIndicator>();
                    break;
                case ISpellIndicator.Type.FixedLinear:
                    var indicator = IndicatorSys.Instance.NewEntity<LinearIndicator>();
                    indicator.Length = data.length;
                    e.indicator = indicator;
                    break;
            }
        }
    }
}
