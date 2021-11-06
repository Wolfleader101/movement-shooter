using UnityEngine;

namespace ScriptableObjects.Items.Bullets
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "Items/Bullet")]
    public class BaseBullet : BaseItem
    {
        [SerializeField] private float speed = 100f;
        public float Speed => speed;
        
        [SerializeField]
        private int damage =  15;
        public int Damage => damage;

        [SerializeField] private float destroyTime = 8f;
        public float DestroyTime => destroyTime;
    
        [SerializeField] private float casingDestroyTime = 4f;
        public float CasingDestroyTime => casingDestroyTime;
        
        [SerializeField] private GameObject bulletPrefab;
        public GameObject BulletPrefab => bulletPrefab;

        [SerializeField] private GameObject bulletCasingPrefab; 
        public GameObject BulletCasingPrefab => bulletCasingPrefab;
    }
}