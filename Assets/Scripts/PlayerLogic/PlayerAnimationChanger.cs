using System;
using EnemyLogic;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimationChanger : MonoBehaviour
    {
        private Animator _animator;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            SwipeDetector.OnSwipe += SetAnimatorValues;
        }
        
        private void OnDisable()
        {
            SwipeDetector.OnSwipe -= SetAnimatorValues;
        }

        private void Update()
        {
            _animator.SetBool("IsMoving", _playerMovement.IsMoving);
        }

        private void SetAnimatorValues(SwipeData swipeData)
        {
            if (_playerMovement.IsMoving == true)
                return;
            
            Vector2 moveDirection = swipeData.SnappingDirection;

            _animator.SetFloat("Horizontal", moveDirection.x);
            _animator.SetFloat("Vertical", moveDirection.y);
        }
    }
}
