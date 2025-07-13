using System;
using GameBase.Math;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Object
{
    /*
     * @FUNC: 用于移动的组件，该组件可实现物理（游戏内）移动
     * @COMMENT: 外部传入移动目标地址，（同一帧内可能存在多个命令，例如移动时按下闪烁键），内部计算出下一帧物体的位置，和终点位置
     * @COMMENT: 依赖组件：rigibody，可选依赖组件rotateComponent
     */
    public partial class MoveComponent : GComponent
    {
        public float maxLeapHeight = 1f;          // 每帧移动最大可以跨越的高度
        

        private Vector3 _moveDest;                // 移动目标点
        private bool _isMovingDest;               // 是否正在移动
        private GObject _followTarget;            // 跟随目标
        private bool _isFollowingTarget;          // 是否正在跟随
        private CircleCollider2D _thisCollider;   // 自身碰撞器
        private CircleCollider2D _targetCollider; // 目标碰撞器
        
        private Vector3 _moveDir;                 // 移动方向

        public float moveSpeed;          // 移动速度
        public Action OnStop;

        private MoveTasks _moveTasks = new MoveTasks();  // 当前所有移动任务

        public bool IsMoving { get => _isMovingDest || _isFollowingTarget; }

        /*
         * @FUNC 停止正在进行的移动指令
         * @COMMENT 也会停止转向
         */
        public void Stop()
        {
            _isMovingDest = false;
            _isFollowingTarget = false;
            _moveDest = transform.position;
            OnStop?.Invoke();
        }
        private void MoveToWithoutRotate(Vector3 position)
        {
            _isMovingDest = true;
        }

        /// <summary>
        /// <list type="bullet">
        /// <item>使得单位可以移动到position</item>
        /// <item>当单位被障碍阻碍移动时，单位应该能自动避让</item>
        /// <item>单位移动到目标位置时停下</item>
        /// </list>
        /// <list type="bullet">
        /// <item><param name="position"><paramref name="position"/>:目标位置</param></item>
        /// </list></summary>
        public void MoveTo(Vector3 position)
        {
            _moveDest = position;
            if (_hasRotateComponent)
            {
                MoveToWithRotate(position);
            }
            else
            {
                MoveToWithoutRotate(position);
            }
        }

        /// <summary>
        /// <list type="bullet">
        /// <item>使得单位远离position</item>
        /// <item>当单位被障碍阻碍移动时，单位应该能自动避让</item>
        /// <item>与position越近，远离的距离越远</item>
        /// </list>
        /// <list type="bullet">
        /// <item><param name="position"><paramref name="position"/>:远离位置</param></item>
        /// </list></summary>
        public void MoveAway(Vector3 position)
        {
            _moveDest = transform.position * 2 - position;
            if (_hasRotateComponent)
            {
                MoveToWithRotate(position);
            }
            else
            {
                MoveToWithoutRotate(position);
            }
        }
        /*
         * @FUNC 使得单位可以立即移动到position
         * @COMMENT 符合物理规则
         */
        public void MoveToImmediate(Vector3 position)
        {
            _moveTasks.Add(0, position);
        }

        public void Follow(GObject target)
        {
            _followTarget = target;
            _thisCollider = gameObject.GetComponent<CircleCollider2D>();
            _targetCollider = target.GetComponent<CircleCollider2D>();
        }

        protected void Awake()
        {
            if (usePhysic)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
            _rotateComponent = GetComponent<RotateComponent>();
            _hasRotateComponent = _rotateComponent != null;

            if (_rigidbody == null && usePhysic)
            {
                Debug.LogWarning("Warning! Physic Move object must has rigidbody");
            }
        }

        private bool IsArrive()
        {
            float stopDistance = moveSpeed * Time.deltaTime;
            float stopDisSqr = stopDistance * stopDistance;
            if (_isMovingDest)
            {
                return GPos.DisVec2Sqr(transform.position, _moveDest) <= stopDisSqr;
            }
            if (_isFollowingTarget)
            {
                return GPos.DisVec2(transform.position, _followTarget.transform.position) 
                    <= stopDistance + _thisCollider.radius + _targetCollider.radius;
            }
            return true;
        }

        private void MoveUpdate()
        {
            if (_isMovingDest) // 计算移动方向，优先计算静止移动方位
            {
                _moveDir = _moveDest - transform.position;
            }
            else if (_isFollowingTarget)
            {
                _moveDir = _followTarget.transform.position - transform.position;
            }
            else
            {
                return;
            }

            if (IsArrive())
            {
                Stop();
            }

            if (IsMoving)
            {
                _moveTasks.Add(1, 
                    transform.position + 
                    _moveDir.normalized * moveSpeed * Time.fixedDeltaTime * TurnMoveSpeedFactor
                    );
            }

            // 如果没有移动人物则提前返回
            if (_moveTasks.Count <= 0) return;

            // 期望的位置就是移动队列的头
            _moveTasks.Sort();

            MovePosition(_moveTasks[0].dest);

            _moveTasks.Clear();
        }

        protected void FixedUpdate()
        {
            MoveUpdate();
        }
    }
}
