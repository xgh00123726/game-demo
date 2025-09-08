namespace GameBase.Shops
{
    public interface IShopInteractive
    {
        bool TrigRefresh {  get; }
        int CurrentPurchase { get; }
    }
}
