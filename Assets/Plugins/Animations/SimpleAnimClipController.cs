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
                    XLogger.Instance.IF(false).Log($"clip:{clipName} set trigger, frame:{Time.frameCount}");
                }

                if (isAnim != lastAnim)
                {
                    animator.SetBool($"Loop_{clipName}", isAnim);
                    XLogger.Instance.IF(false).Log($"clip:{clipName} set loop:{isAnim}, frame:{Time.frameCount}");
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
