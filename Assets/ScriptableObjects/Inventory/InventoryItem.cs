using ScriptableObjects.Items;
using Sirenix.OdinInspector;

namespace ScriptableObjects.Inventory
{
    public class InventoryItem
    {
        [ShowInInspector]
        public BaseItem Item { get; set; }
        
        [ShowInInspector]
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