namespace GameBase.Shops
{
    public enum ModelType
    {
        Common,
    }
    public class ShopModelFactory
    {
        public static ShopModel Get(ModelType modelType, int id)
        {
            if (modelType == ModelType.Common)
            {
                return new ShopModel($"CommonShop_{id}.csv");
            }

            return null;
        }
    }
}
