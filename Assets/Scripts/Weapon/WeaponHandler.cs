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
    [SerializeField] private Transform gunPos;

    private int _currentAmmo;
    private float _currentCooldown;

    private bool _holdingShoot = false;
    private float _playerAccuracy = 0f;

    private Camera _camera;

    private ObjectPool<GameObject> _bulletPool;
    private ObjectPool<GameObject> _bulletCasingPool;


    private void Start()
    {
        _currentAmmo = weapon.AmmoCapacity;
        _currentCooldown = weapon.FireRate;

        firstPersonController.OnShootEvent += Shoot;

        _camera = Camera.main;

        Instantiate(weapon.Prefab, gunPos);
    }

    private void Update()
    {
        if (!_holdingShoot) return;
        if (weapon.IsHitScan)
        {
            ShootHitScan();
        }
        else
        {
            ShootProjectile();
        }
    }

    private void Shoot(bool shooting, bool holdingShoot, float playerAccuracy)
    {
        _holdingShoot = holdingShoot;
        _playerAccuracy = playerAccuracy;

        if (!shooting) return;
        
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
        var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out var hit))
        {
            Debug.DrawRay(ray.origin, transform.forward * hit.distance, Color.red, 1.5f);
            Debug.Log($"Hit {hit.collider.gameObject.name}");
        }
    }

    private void ShootProjectile()
    {
    }
}