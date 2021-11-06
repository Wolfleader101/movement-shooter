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
                return TryAddItem(item, itemCount);
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

            return TryAddItem(item, itemCount);
        }

        private int TryAddItem(BaseItem item, int itemCount)
        {
            if (_items.Count >= maxCapacity) return itemCount;

            InventoryItem invItem;
            while (itemCount > item.MaxStackSize)
            {
                if (_items.Count >= maxCapacity) return itemCount;

                invItem = new InventoryItem(item, item.MaxStackSize);
                _items.Add(invItem);
                
                OnItemAdded?.Invoke(invItem, item.MaxStackSize);
                    
                itemCount -= item.MaxStackSize;
            }
                
            if (_items.Count >= maxCapacity) return itemCount;

            invItem = new InventoryItem(item, itemCount);
            _items.Add(invItem);

            OnItemAdded?.Invoke(invItem, itemCount);
            return 0;
        }
        
        private int AttempToAddItem(BaseItem item, int itemCount)
        {
            if (_items.Count >= maxCapacity) return itemCount;

            var clamped = Mathf.Clamp(itemCount, 1, item.MaxStackSize);
            InventoryItem invItem = new InventoryItem(item, clamped);
            _items.Add(invItem);
            
            OnItemAdded?.Invoke(invItem, clamped);

            itemCount -= clamped;
            if (itemCount != 0)
            {
                return AttempToAddItem(item, itemCount);
            }

            return itemCount;
        }

        

        // public void RemoveItem(BaseItem item, int count)
        // {
        //     if (!_items.ContainsKey(item)) return;
        //
        //     if (_items[item] - count <= 0)
        //     {
        //         _items.Remove(item);
        //         OnItemRemoved?.Invoke(item, count);
        //         return;
        //     }
        //
        //     _items[item] -= count;
        //     OnItemRemoved?.Invoke(item, count);
        //     return;
        // }
    
    }
}