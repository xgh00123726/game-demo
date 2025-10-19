using GameBase.Tools;

namespace GameBase.EntitySystem
{
    public class SingletonInstance<T> : Singleton<T>
        where T : new()
    {
        protected virtual bool IsUpdate => true;
        protected virtual bool IsFixedUpdate => false;

        private int _updateActionIndex = -1;
        private int _fixedUpdateActionIndex = -1;

        public SingletonInstance()
        {
            if (IsUpdate)
            {
                _updateActionIndex = SingletonInstanceUpdateBehavior.RegisterUpdate(Update, typeof(T).Name);
            }

            if (IsFixedUpdate)
            {
                _fixedUpdateActionIndex = SingletonInstanceFixedUpdateBehavior.RegisterUpdate(FixedUpdate, typeof(T).Name);
            }
        }

        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }

        public void SetActive(bool active)
        {
            if (_updateActionIndex < 0)
            {
                return;
            }
            SingletonInstanceUpdateBehavior.SetActive(_updateActionIndex, active);
        }
        public bool ActiveSelf
        {
            get
            {
                if (_updateActionIndex < 0)
                {
                    return false;
                }
                return SingletonInstanceUpdateBehavior.ActiveSelf(_updateActionIndex);
            }
        }

        public void SetFixedActive(bool active)
        {
            if (_fixedUpdateActionIndex < 0)
            {
                return;
            }
            SingletonInstanceFixedUpdateBehavior.SetActive(_fixedUpdateActionIndex, active);
        }
        public bool FixedActiveSelf
        {
            get
            {
                if (_fixedUpdateActionIndex < 0)
                {
                    return false;
                }
                return SingletonInstanceFixedUpdateBehavior.ActiveSelf(_updateActionIndex);
            }
        }
    }
}
