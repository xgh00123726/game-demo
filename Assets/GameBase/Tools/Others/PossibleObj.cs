namespace GameBase.Tools
{
    public struct PossibleObj<T>
    {
        T obj;
        private bool _objExist;
        public bool Exist => _objExist;

        public static PossibleObj<T> New(T obj)
        {
            var ret = new PossibleObj<T>();
            ret.Set(obj);
            return ret;
        }

        public T Get() => obj;
        public void Set(T obj)
        {
            this.obj = obj;
            if (obj == null)
            {
                _objExist = false;
            }
            else
            {
                _objExist = true;
            }
        }
    }
}
