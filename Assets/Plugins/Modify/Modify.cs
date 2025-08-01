namespace GameBase.Modify
{
    public class Modify
    {
        protected IModifyable _target;

        /// <summary>
        /// 将一个Modify作用于目标
        /// 
        /// <list type="bullet">
        /// <item><param name="o"><paramref name="o"/>:目标对象</param></item>
        /// </list></summary>
        public void ModifyTo(IModifyable o)
        {
            _target = o;
            ModifyMgr.Add(this);
        }

        /// <summary>
        /// 销毁自身
        /// </summary>
        public void Destroy()
        {
            ModifyMgr.Remove(this);
        }

        /// <summary>
        /// Modify每个周期都会重置，由ModifyMgr执行
        /// </summary>
        internal void Reset()
        {
            _target.attrs.Reset();
        }

        internal void Update()
        {
            OnModify();
        }

        protected virtual void OnModify()
        {

        }
    }
}
