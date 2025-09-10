namespace Instance.MVC
{
    public interface IInventoryModel<T>
    {
        T this[int index] { get; }
        int Size { get; set; }
        bool HasItem(int index);
        int AddItem(T item);
        int AddItem(T item, int index);
        void RemoveItem(int index);
        void Swap(int p1, int p2);
    }
}
