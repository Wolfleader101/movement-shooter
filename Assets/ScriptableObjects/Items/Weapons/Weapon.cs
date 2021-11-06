using UnityEngine;
using UnityEngine.Internal;

namespace ScriptableObjects.Items.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class Weapon : BaseItem
    {
        [SerializeField] private int fireRate;
    }
}