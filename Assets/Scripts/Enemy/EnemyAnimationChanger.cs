using System;
using Player;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationChanger : MonoBehaviour
    {
        private Animator _animator;
        private Vector2 _previousPosition;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _previousPosition = transform.position;
        }

        private void Update()
        {
            SetAnimatorValues();
        }

        private void SetAnimatorValues()
        {
            Vector2 moveDirection = new Vector2(transform.position.x - _previousPosition.x,
                transform.position.y - _previousPosition.y);

            if (Vector2.Distance(transform.position, _previousPosition) > Mathf.Epsilon)
            {
                _animator.SetFloat("Horizontal", moveDirection.x);
                _animator.SetFloat("Vertical", moveDirection.y);
                _animator.SetFloat("Speed", moveDirection.magnitude);
            }

            _previousPosition = transform.position;
        }
    }
}
