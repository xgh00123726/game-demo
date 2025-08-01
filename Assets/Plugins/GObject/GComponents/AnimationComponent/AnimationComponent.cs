using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.Object
{
    public partial class AnimationComponent : GComponent
    {
        public Animator _animator;
        public AnimationClip[] _animationClips;
        public string _defaultClipName;

        public float animatorNormalizedTime;
        public float _resetPeriod = 1f;
        public bool _resetEnable = true;
        
        public AnimatorStateInfo _animatorStateInfo;

        public bool ResetEnable
        {
            get => _resetEnable;
            set => _resetEnable = value;
        }
        public float ResetPeriod
        {
            get => _resetPeriod;
            set => _resetPeriod = value;
        }
        
        // Start is called before the first frame update
        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            Assert.IsNotNull(_animator);

            _animationClips = _animator.runtimeAnimatorController.animationClips;
        }

        public void SetClipRun(string name)
        {
            if (_animatorStateInfo.IsName(name)) return;

            ForceSetClipRun(name);
        }

        public void ForceSetClipRun(string name)
        {
            string triggerName = "Trigger_" + name;

            _animator.SetTrigger(triggerName);
        }


        public void EnableClipLoop(string name)
        {
            string loopName = "Loop_" + name;

            _animator.SetBool(loopName, true);
        }

        public void DisableClipLoop(string name)
        {
            string loopName = "Loop_" + name;

            _animator.SetBool(loopName, false);
        }

        public void SetDefaultClip(string name)
        {
            _defaultClipName = name;
        }

        // Update is called once per frame
        protected void Update()
        {
            _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            animatorNormalizedTime = _animatorStateInfo.normalizedTime;

            if (!_resetEnable) return;
            if (_defaultClipName == null) return;
            if (_animatorStateInfo.IsName(_defaultClipName)) return;
            if (animatorNormalizedTime > _resetPeriod)
            {
                SetClipRun(_defaultClipName);
            }
        }
    }
}
