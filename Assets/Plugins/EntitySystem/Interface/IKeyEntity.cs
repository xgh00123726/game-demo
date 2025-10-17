namespace GameBase.EntitySystem
{
    public interface IKeyEntity<T_Key>
    {
        T_Key Key { get; set; }
    }
}
