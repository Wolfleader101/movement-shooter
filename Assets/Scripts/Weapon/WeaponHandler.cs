using System;
using ScriptableObjects.Items.Weapons;
using UnityEngine;
using UnityEngine.Pool;


[RequireComponent(typeof(FirstPersonController))]
public class WeaponHandler : MonoBehaviour
{
    // eventually have a list of weapons from inventory to grab from
    [SerializeField] private BaseWeapon weapon;
    [SerializeField] private FirstPersonController firstPersonController;

    private int _currentAmmo;
    private float _currentCooldown;
    
    private ObjectPool<GameObject> _bulletPool;
    private ObjectPool<GameObject> _bulletCasingPool;

    
    private void Start()
    {
        _currentAmmo = weapon.AmmoCapacity;
        _currentCooldown = weapon.FireRate;
        
        firstPersonController.OnShootEvent += Shoot;
    }


    private void Shoot()
    {
        if (weapon.IsHitScan)
        {
            ShootHitScan();
        }
        else
        {
            ShootProjectile();
        }
    }

    private void ShootHitScan()
    {
    }

    private void ShootProjectile()
    {
    }
}