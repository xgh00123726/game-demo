namespace GameBase.Tools
{
    public interface IPoolableObject 
    {
        void OnInstantiate();
        void OnRelease();
    }
}
