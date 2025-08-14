namespace GameBase.Tools
{
    /// <summary>
    /// <list type="bullet">
    /// <item>unity object判空会带来额外的消耗</item>
    /// <item>对于需要周期判空的unity object，使用possible object进行快捷判空</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct PossibleObj<T>
    {
        T obj;
        private bool _objExist;
        public bool Exist => _objExist;

        public static implicit operator PossibleObj<T>(T obj) => New(obj);
        public static PossibleObj<T> New(T obj)
        {
            var ret = new PossibleObj<T>();
            ret.Set(obj);
            if (obj == null)
            {
                ret._objExist = false;
            }
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
