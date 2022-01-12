using System;
using UnityEngine;

namespace Player
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

        private void SetAnimatorValues(SwipeData swipeData)
        {
            if (_playerMovement.IsMoving == true)
                return;
            
            Vector2 moveDirection = GetMoveDirection(swipeData);

            _animator.SetFloat("Horizontal", moveDirection.x);
            _animator.SetFloat("Vertical", moveDirection.y);
            _animator.SetFloat("Speed", moveDirection.magnitude);
        }

        private Vector2 GetMoveDirection(SwipeData swipeData)
        {
            Vector2 moveDirection = new Vector2();
            
            if (swipeData.Direction == SwipeDirection.Right)
                moveDirection = Vector2.right;
            else if (swipeData.Direction == SwipeDirection.Left)
                moveDirection = Vector2.left;
            else if (swipeData.Direction == SwipeDirection.Up)
                moveDirection = Vector2.up;
            else if (swipeData.Direction == SwipeDirection.Down)
                moveDirection = Vector2.down;

            return moveDirection;
        }
    }
}
