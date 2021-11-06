using UnityEngine;

namespace ScriptableObjects.Items
{
    public abstract class BaseItem : ScriptableObject
    {
        [SerializeField] protected string itemName;
        public string ItemName => itemName;
    
        [SerializeField, Multiline] protected string description;
        public string Description => description;

        [SerializeField] protected int maxStackSize = 32;
        public int MaxStackSize => maxStackSize;


        [SerializeField] protected GameObject prefab;
        public GameObject Prefab => prefab;
        
        [SerializeField] protected Sprite sprite;
        public Sprite Sprite => sprite;

        [SerializeField] protected Sprite stackedSprite;
        public Sprite StackedSprite => stackedSprite;
    }
}