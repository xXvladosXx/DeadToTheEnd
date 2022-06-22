namespace GameCore.ShopSystem
{
    public class Buyer : Transferer
    {
        public override bool HasEnoughResources()
        {
            return true;
        }

        public override void Transfer()
        {
            
        }
    }
}