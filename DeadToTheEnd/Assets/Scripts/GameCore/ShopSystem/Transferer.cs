namespace GameCore.ShopSystem
{
    public abstract class Transferer
    {
        public abstract bool HasEnoughResources();
        public abstract void Transfer();
    }
}