using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerStats _playerStats;
        private Vector2 _endPosition;
        private bool _isMoving = false;


        public bool IsMoving => _isMoving;
        
        
        private void Awake()
        {
            _playerStats = GetComponent<PlayerStats>();
        }

        private void OnEnable()
        {
            SwipeDetector.OnSwipe += Move;
        }

        private void OnDisable()
        {
            SwipeDetector.OnSwipe -= Move;
        }

        private void Move(SwipeData swipeData)
        {
            if (_isMoving == true)
                return;
            
            if (swipeData.Direction == SwipeDirection.Right)
                _endPosition = (Vector2)transform.position + Vector2.right;
            else if (swipeData.Direction == SwipeDirection.Left)
                _endPosition = (Vector2)transform.position + Vector2.left;
            else if (swipeData.Direction == SwipeDirection.Up)
                _endPosition = (Vector2)transform.position + Vector2.up;
            else if (swipeData.Direction == SwipeDirection.Down)
                _endPosition = (Vector2)transform.position + Vector2.down;

            StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            _isMoving = true;
            
            while ((Vector2)transform.position != _endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _endPosition,
                    _playerStats.MoveSpeed * Time.fixedDeltaTime);
                yield return null;
            }

            _isMoving = false;
        }
    }
}
