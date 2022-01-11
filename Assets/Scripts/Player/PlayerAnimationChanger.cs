using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationChanger : MonoBehaviour
    {
        private Joystick _joystick;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private Vector2 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _joystick = FindObjectOfType<Joystick>();
        }

        private void Update()
        {
            SetMoveDirection();
            
            SetAnimatorValues();
        }

        private void SetMoveDirection()
        {
            _moveDirection.x = _joystick.Horizontal;
            _moveDirection.y = _joystick.Vertical;
        }

        private void SetAnimatorValues()
        {
            _animator.SetFloat("Horizontal", _moveDirection.x);
            _animator.SetFloat("Vertical", _moveDirection.y);
            _animator.SetFloat("Speed", _moveDirection.magnitude);
        }
    }
}
