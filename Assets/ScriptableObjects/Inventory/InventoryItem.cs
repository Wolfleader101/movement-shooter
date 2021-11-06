using ScriptableObjects.Items;

namespace ScriptableObjects.Inventory
{
    public class InventoryItem
    {
        public BaseItem Item { get; set; }
        public int ItemCount { get; set; }

        public InventoryItem()
        {
            
        }

        public InventoryItem(BaseItem item, int itemCount)
        {
            Item = item;
            ItemCount = itemCount;
        }
        
    }
}