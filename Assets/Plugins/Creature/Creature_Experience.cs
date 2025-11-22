using GameBase.Tools;
using GameBase.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GameBase.Creatures
{
    public partial class Creature
    {
        public int CurrentExp { get; private set; }
        public int DeadExp { get; set; } = 1;
        public List<int> LevelUpExpTable { get; set; } = new();
        public int CurrentLevel { get; private set; } = 1;
        public int LevelUpExp { get; private set; } = 10;

        public Action<Creature> OnLevelUp {  get; set; }
        public Action<Creature, int> OnGetExp { get; set; }

        public void SetDefaultGetExpText()
        {
            OnGetExp = DefaultOnGetExp;
        }

        public void DefaultOnGetExp(Creature creature, int exp)
        {
            var text = TextSys.Instance.NewEntity("Prefabs/UI/FloatText");
            text.Color = Color.blue;
            text.showPosition = Position;
            text.Value = exp.ToString();
        }

        public void RefreshLevelUpExp()
        {
            if (LevelUpExpTable.Count >= CurrentLevel)
            {
                LevelUpExp = LevelUpExpTable[CurrentLevel - 1];
            }
            else
            {
                LevelUpExp = CurrentLevel * 10;
            }
        }

        public void GetExp(int exp)
        {
            CurrentExp += exp;
            OnGetExp?.Invoke(this, exp);
            RefreshLevelUpExp();

            while (CurrentExp >= LevelUpExp)
            {
                CurrentExp -= LevelUpExp;
                CurrentLevel++;
                OnLevelUp?.Invoke(this);
            }
        }
    }
}
