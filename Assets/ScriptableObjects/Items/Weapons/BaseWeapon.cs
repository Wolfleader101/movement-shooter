using ScriptableObjects.Items.Bullets;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Internal;

namespace ScriptableObjects.Items.Weapons
{
    public enum FiringType
    {
        Tap,
        Burst,
        Auto
    }
    
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class BaseWeapon : BaseItem
    {
        [SerializeField] private int ammoCapacity = 30;
        public int AmmoCapacity => ammoCapacity;

        [SerializeField] private int bulletsToFire = 1;
        public int BulletsToFire => bulletsToFire;

        [SerializeField] private FiringType firingType = FiringType.Tap;
        public FiringType FiringType => firingType;
    
        [SerializeField] private float fireRate = 0.1f; // fire rate in seconds. (how often to shoot a bullet)
        public float FireRate => fireRate;
        
        // accuracy

        [SerializeField] private float reloadTime = 2.3f; // reload time in seconds
        public float ReloadTime => reloadTime;

        [SerializeField] private bool isHitScan = true;
        public bool IsHitScan => isHitScan;
        
        [SerializeField, HideIf("isHitScan")] private BaseBullet bullet;
        public BaseBullet Bullet => bullet;

        [SerializeField] private Transform firePoint;
        public Transform FirePoint => firePoint;
        
        [SerializeField] private Transform casingPoint;
        public Transform CasingPoint => casingPoint;

        // shoot animation?
        // reload animation?
    }
}