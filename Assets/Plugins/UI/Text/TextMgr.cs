using System;
using System.Collections.Generic;
using UnityEngine;
using GameBase.LifeTime;
using GameBase.Resources;
using GameBase.Tools;

namespace GameBase.UI
{
    public class TextMgr : IManager
    {
        private static LinkedList<Action> _textUpdates = new LinkedList<Action>();
        private static PoolableMonoMgr<DamageText> _damageTextMgr;

        static TextMgr()
        {
            LifeTimeMgr.RegisterMgr(new TextMgr());
            _damageTextMgr = PoolableMonoMgr<DamageText>.Instance(PrefabType.UI);
        }

        internal static void RegisterTextUpdate(Action updateAction)
        {
            _textUpdates.AddLast(updateAction);
        }

        public static void ShowText(Vector3 position, string text)
        {
            var damageText = _damageTextMgr.Get();
            damageText.ShowPosition = position;
            damageText.Text = text;
            Timer.AddTask(UnityEngine.Random.Range(DamageText.durationMin, DamageText.durationMax),
                () => _damageTextMgr.Release(damageText));
        }

        public static void ShowDamageText(Vector3 position, float value)
        {
            var damageText = _damageTextMgr.Get();
            damageText.ShowPosition = position;
            damageText.Text = value.ToString();
            Timer.AddTask(UnityEngine.Random.Range(DamageText.durationMin, DamageText.durationMax), 
                () => _damageTextMgr.Release(damageText));
        }


        void IManager.Update()
        {
            foreach (var action in _textUpdates)
            {
                action();
            }
        }
    }
}
