using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Items;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(menuName = "Inventory")]
    public class Inventory : ScriptableObject
    {
         public event Action<InventoryItem, int> OnItemAdded;
        public event Action<InventoryItem, int> OnItemRemoved;

        [SerializeField] private int maxCapacity = 36;
        public int MaxCapacity => maxCapacity;

        private List<InventoryItem> _items;
        public List<InventoryItem> Items => _items;

        public void Init()
        {
            _items = new List<InventoryItem>(maxCapacity);
        }

        public int AddItem(BaseItem item, int itemCount)
        {
            
            if (_items.Find(itemInList => itemInList.Item.Equals(item)) == null)
            {
                return AddNewItem(item, itemCount);
            }

            foreach (var itemInList in _items.Where(queuedItem => queuedItem.Item.Equals(item)))
            {
                // if its already at max cap
                if (itemInList.ItemCount >= item.MaxStackSize) continue;
                
                var maxIncrement = item.MaxStackSize - itemInList.ItemCount;
                var clamped = Mathf.Clamp(itemCount, 1, maxIncrement);
                
                itemInList.ItemCount += clamped;
                OnItemAdded?.Invoke(itemInList, clamped);

                itemCount -= clamped;
                if (itemCount != 0) continue;
                
                return 0;
            }

            return AddNewItem(item, itemCount);
        }
        
        private int AddNewItem(BaseItem item, int itemCount)
        {
            while (true)
            {
                if (_items.Count >= maxCapacity) return itemCount;

                var clamped = Mathf.Clamp(itemCount, 1, item.MaxStackSize);
                var invItem = new InventoryItem(item, clamped);
                _items.Add(invItem);

                OnItemAdded?.Invoke(invItem, clamped);

                itemCount -= clamped;
                if (itemCount != 0) continue;
                return itemCount;
            }
        }
        
        public int RemoveItem(BaseItem item, int itemCount)
        {
            if (!_items.Any(invItem => invItem.Item.Equals(item))) return itemCount;

            var inventoryItems = _items.FindAll(invItem => invItem.Item.Equals(item)).OrderByDescending(invItem => invItem.ItemCount).ToList();
            
            foreach (var inventoryItem in inventoryItems)
            {
                if (inventoryItem.ItemCount - itemCount <= 0)
                {
                    itemCount -= inventoryItem.ItemCount;
                    _items.Remove(inventoryItem);
                    OnItemRemoved?.Invoke(inventoryItem, itemCount);
                    continue;
                } 
                
                inventoryItem.ItemCount -= itemCount;
                itemCount -= itemCount;
                OnItemRemoved?.Invoke(inventoryItem, itemCount);
                break;
            }


            return itemCount;
        }
    
    }
}