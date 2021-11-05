using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableObjects.Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "Character")]
    public class BaseCharacter : ScriptableObject
    {
        struct Stats
        {
            [ShowInInspector]
            private float _startingHealth;
           private float _startingStamina;
        }
        
       [ShowInInspector] 
       private Stats stats;
    }
}