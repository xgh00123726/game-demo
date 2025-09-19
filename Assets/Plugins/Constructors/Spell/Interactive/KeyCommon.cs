using GameBase.Indicators;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;
using System.Collections.Generic;

namespace Constructor.Spells.Interactive
{
    public struct CommonData
    {
        public Indicators.Type indicatorType;
        public float radius;
        public float length;
    }

    public class KeyCommon : ISpellInteractive
    {
        internal static List<KeyCommon> mutexInteractives = new();
        internal void DeReadyAll()
        {
            foreach (var interactive in mutexInteractives)
            {
                interactive.indicatorReady = false;
                interactive.indicator?.Hide();
            }
        }

        internal bool indicatorReady;

        public KeyFunction readyKey;
        public KeyFunction castKey;
        public KeyFunction cancelKey;
        public IInteractiveIndicator indicator;
        public bool readyLockEnable = true;
        public bool fastCast;


        bool ReadyTrig
        {
            get
            {
                return fastCast ? true : Inputs.GetKeyDown(readyKey, "spell");
            }
        }

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
                DeReadyAll();
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

        void ISpellInteractive.OnTrig(ISpeller speller)
        {
            indicatorReady = false;
        }

        public static void SetHotKey(ISpellInteractive interactive, KeyFunction function)
        {
            if (interactive is KeyCommon keyCommon)
            {
                keyCommon.readyKey = function;
            }
            else if (interactive is KeyFast keyFast)
            {
                keyFast.castKey = function;
            }
        }
    }

    public class KeyCommonCon : BaseConstructor<CommonData, KeyCommon, KeyCommonCon>
    {
        protected override string RelativePath => "Spell/Interactive/KeyCommon.csv";

        protected override KeyCommon Get()
        {
            return new KeyCommon();
        }

        protected override void Parse(CsvReader line, ref CommonData data)
        {
            Enum.TryParse(line[1], out data.indicatorType);
            data.radius = float.Parse(line[2]);
            data.length = float.Parse(line[3]);
        }

        protected override void Set(KeyCommon e, in CommonData data)
        {
            e.cancelKey = KeyFunction.Cancel;
            e.castKey = KeyFunction.MouseConfirm;

            var idata = new Indicators.IndicatorData();
            idata.radius = data.radius;
            idata.length = data.length;
            e.indicator = Constructor.Spells.Indicators.Factory.Get(data.indicatorType, idata);
            e.indicator.Hide();
            if (e.readyLockEnable)
            {
                KeyCommon.mutexInteractives.Add(e);
            }
        }
    }
}
