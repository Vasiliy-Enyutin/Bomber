using UnityEngine;

namespace PlayerLogic
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _abilityReloadTime;

        public float MoveSpeed => _moveSpeed;

        public float AbilityReloadTime => _abilityReloadTime;
    }
}
