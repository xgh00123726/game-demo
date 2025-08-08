namespace GameBase.Modify
{
    public interface IModifyOwner<T>
    {
        ModifyableContainer<T> Modifyables { get; }
    }
}
