namespace GameBase.Modify
{
    public class GModify
    {
        protected IModifyable _target;
        public void ModifyTo(IModifyable o)
        {
            _target = o;
            ModifyMgr.Add(this);
        }
        public void Destroy()
        {
            ModifyMgr.Remove(this);
        }

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
