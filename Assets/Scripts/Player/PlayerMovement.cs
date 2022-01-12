using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private LayerMask _unwalkableMask;
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
            
            Vector2 direction = new Vector2();
            if (swipeData.Direction == SwipeDirection.Right)
                direction = Vector2.right;
            else if (swipeData.Direction == SwipeDirection.Left)
                direction = Vector2.left;
            else if (swipeData.Direction == SwipeDirection.Up)
                direction = Vector2.up;
            else if (swipeData.Direction == SwipeDirection.Down)
                direction = Vector2.down;

            _endPosition = GetEndPosition(direction);
            StartCoroutine(MoveRoutine());
        }

        private Vector2 GetEndPosition(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, _unwalkableMask);
            if (hit.collider != null)
                return transform.position;
            
            return (Vector2)transform.position + direction;
        }

        private IEnumerator MoveRoutine()
        {
            _isMoving = true;
            
            while (Vector2.Distance(transform.position, _endPosition) > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, _endPosition,
                    _playerStats.MoveSpeed * Time.fixedDeltaTime);
                yield return null;
            }

            transform.position = _endPosition;
            _isMoving = false;
        }
    }
}
