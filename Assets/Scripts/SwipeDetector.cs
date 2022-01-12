using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] private bool _detectSwipeOnlyAfterRelease = false;
    [SerializeField] private float _minDistanceForSwipe = 20f;
    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;

    public static event Action<SwipeData> OnSwipe;
    

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerUpPosition = touch.position;
                _fingerDownPosition = touch.position;
            }

            if (_detectSwipeOnlyAfterRelease == false && touch.phase == TouchPhase.Moved)
            {
                _fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceGreaterThanMinDistance() == false) 
            return;
        
        if (IsVerticalSwipe() == true)
        {
            SwipeDirection direction = _fingerDownPosition.y - _fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            SendSwipe(direction);
        }
        else
        {
            SwipeDirection direction = _fingerDownPosition.x - _fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
            SendSwipe(direction);
        }
        _fingerUpPosition = _fingerDownPosition;
    }

    private bool IsVerticalSwipe()
    {
        return GetVerticalMovementDistance() > GetHorizontalMovementDistance();
    }

    private bool SwipeDistanceGreaterThanMinDistance()
    {
        return GetVerticalMovementDistance() > _minDistanceForSwipe || GetHorizontalMovementDistance() > _minDistanceForSwipe;
    }

    private float GetVerticalMovementDistance()
    {
        return Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);
    }

    private float GetHorizontalMovementDistance()
    {
        return Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = _fingerDownPosition,
            EndPosition = _fingerUpPosition
        };
        OnSwipe?.Invoke(swipeData);
    }
}
