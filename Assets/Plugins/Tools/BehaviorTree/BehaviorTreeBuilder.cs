using System;
using System.Collections.Generic;
using static GameBase.Tools.Behavior;

namespace GameBase.Tools
{
    public class BehaviorTreeBuilder
    {
        private BehaviorTree _tree = new BehaviorTree(null);
        public BehaviorTree Tree => _tree;
        private Stack<Behavior> _nodeStack = new Stack<Behavior>();

        private void AddBehavior(Behavior behavior)
        {
            // 第一个节点作为根节点
            if (_tree._root == null)
            {
                _tree._root = behavior;
            }
            // 向最近的父节点添加子节点
            else
            {
                _nodeStack.Peek().AddChild(behavior);
            }

            // 如果是组合节点或者修饰器节点，则将其作为最近的父节点
            if (behavior is Composite || behavior is Decorator)
            {
                _nodeStack.Push(behavior);
            }
        }

        public BehaviorTreeBuilder TickRate(int tickRate)
        {
            _tree.tickRate = tickRate;
            return this;
        }

        public BehaviorTreeBuilder Back()
        {
            _nodeStack.Pop();
            return this;
        }
        
        public BehaviorTreeBuilder End()
        {
            _nodeStack.Clear();
            return this;
        }

        public BehaviorTreeBuilder Sequence()
        {
            var bh = new Sequence();
            AddBehavior(bh);
            return this;
        }

        public BehaviorTreeBuilder Selector()
        {
            var bh = new Selector();
            AddBehavior(bh);
            return this;
        }

        public BehaviorTreeBuilder Inverter()
        {
            var bh = new Inverter();
            AddBehavior(bh);
            return this;
        }

        public BehaviorTreeBuilder Repeat(int repeatCount)
        {
            var bh = new Repeat(repeatCount);
            AddBehavior(bh);
            return this;
        }

        public BehaviorTreeBuilder Log(string word)
        {
            var bh = new Log(word);
            AddBehavior(bh);
            return this;
        }

        public BehaviorTreeBuilder Success()
        {
            var bh = new SuccessBehavior();
            AddBehavior(bh);
            return this;
        }

        public BehaviorTreeBuilder IF(Func<bool> condition)
        {
            var bh = new ConditionBehavior(condition);
            AddBehavior(bh);
            return this;
        }

        public BehaviorTreeBuilder RR(Func<Status> RRAction)
        {
            var bh = new RequestResponseBehavior(RRAction);
            AddBehavior(bh);
            return this;
        }

        public BehaviorTreeBuilder FF(Action FFAction)
        {
            var bh = new FireForgetBehavior(FFAction);
            AddBehavior(bh);
            return this;
        }
    }
}
