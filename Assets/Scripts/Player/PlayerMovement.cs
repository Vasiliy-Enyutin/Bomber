using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerMovement : MonoBehaviour
    {
        private Joystick _joystick;
        private Rigidbody2D _rigidbody;
        private PlayerStats _playerStats;
        private Vector2 _moveDirection;
    

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerStats = GetComponent<PlayerStats>();
        }

        private void Start()
        {
            _joystick = FindObjectOfType<Joystick>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _moveDirection = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            // fix increase speed when walking diagonally
            if (Mathf.Sqrt(Mathf.Pow(_moveDirection.x, 2) + Mathf.Pow(_moveDirection.y, 2)) > 1)
                _moveDirection.Normalize();
            _rigidbody.MovePosition(_rigidbody.position + _moveDirection * _playerStats.MoveSpeed * Time.fixedDeltaTime);
        }
    }
}
