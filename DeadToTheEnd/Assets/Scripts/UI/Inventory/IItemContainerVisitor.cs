namespace UI.Inventory
{
    public interface IItemContainerVisitor
    {
        public void Visit(ItemSlotUI itemSlotUI);
        public void Visit(SellerItemSlotUI sellerItemSlotUI);
    }
}