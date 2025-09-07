using GameBase.Indicators;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;

namespace Constructor.Spells.Interactive
{
    public struct CommonData
    {
        public Indicators.Type indicatorType;
        public float radius;
        public float length;
    }

    public class KeyInteractive : ISpellInteractive
    {
        public KeyFunction readyKey;
        public KeyFunction castKey;
        public KeyFunction cancelKey;
        public IInteractiveIndicator indicator;
        public bool fastCast;

        private bool indicatorReady;

        bool ReadyTrig => fastCast ? true : Inputs.GetKeyDown(readyKey, "spell");

        bool CancelTrig => Inputs.GetKeyDown(cancelKey, "spell");

        bool CastTrig
        {
            get
            {
                if (fastCast)
                {
                    return Inputs.GetKeyDown(readyKey, "spell");
                }
                else
                {
                    return indicatorReady && Inputs.GetKeyDown(castKey, "spell");
                }
            }
        }

        bool ISpellInteractive.IsTrig => CastTrig;

        void ISpellInteractive.Update(ISpeller speller)
        {
            if (ReadyTrig)
            {
                indicator?.Show();
                indicatorReady = true;
            }
            if (CancelTrig)
            {
                indicator?.Hide();
                indicatorReady = false;
            }
            if (CastTrig)
            {
                indicator?.Hide();
            }

            indicator?.Update(speller, GameBase.GCamera.CameraSys.MouseHitPosition);
        }
    }

    public class KeyCommon : BaseConstructor<CommonData, KeyInteractive, KeyCommon>
    {
        protected override string RelativePath => "Spell/Interactive/KeyCommon.csv";

        protected override KeyInteractive Get()
        {
            return new KeyInteractive();
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

            var idata = new Indicators.IndicatorData();
            idata.radius = data.radius;
            idata.length = data.length;
            e.indicator = Constructor.Spells.Indicators.Factory.Get(data.indicatorType, idata);
        }
    }
}
