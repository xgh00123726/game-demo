using GameBase.Tools;
using System;
using UnityEngine;

namespace GameBase.Animations
{
    internal class SimpleAnimClipController
    {
        public SimpleAnimClipController(Animator animator, Func<bool> IsAnim, string name)
        {
            this.animator = animator;
            this.IsAnim = IsAnim;
            clipName = name;
        }

        public bool ctrlEnable = true;
        public Func<bool> IsAnim;
        public bool isAnim;
        public bool lastAnim;
        public string clipName;
        public Animator animator;
        public float stateDuration;

        public void Update()
        {
            isAnim = IsAnim();

            if (ctrlEnable)
            {
                if (!lastAnim && isAnim)
                {
                    animator.SetTrigger($"Trigger_{clipName}");
                }

                if (isAnim != lastAnim)
                {
                    animator.SetBool($"Loop_{clipName}", isAnim);
                }
            }

            if (lastAnim == isAnim)
            {
                stateDuration += Time.deltaTime;
            }
            else
            {
                stateDuration = 0;
            }

            lastAnim = isAnim;
        }
    }
}
