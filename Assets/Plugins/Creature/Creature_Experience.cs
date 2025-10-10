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
        public int currentExp;
        public int deadExp = 1;
        public List<int> levelUpExpTable = new();
        public int currentLevel = 1;
        public int levelUpExp = 10;

        public Action<Creature> OnLevelUp;
        public Action<Creature, int> OnGetExp;

        public void SetDefaultGetExpText()
        {
            OnGetExp = DefaultOnGetExp;
        }

        public void DefaultOnGetExp(Creature creature, int exp)
        {
            var text = TextSys.Instance.NewEntity();
            text.Color = Color.blue;
            text.showPosition = Position;
            text.Value = exp.ToString();
        }

        public void RefreshLevelUpExp()
        {
            if (levelUpExpTable.Count >= currentLevel)
            {
                levelUpExp = levelUpExpTable[currentLevel - 1];
            }
            else
            {
                levelUpExp = currentLevel * 10;
            }
        }

        public void GetExp(int exp)
        {
            currentExp += exp;
            OnGetExp?.Invoke(this, exp);
            RefreshLevelUpExp();

            while (currentExp >= levelUpExp)
            {
                currentExp -= levelUpExp;
                currentLevel++;
                OnLevelUp?.Invoke(this);
            }
        }
    }
}
