using System;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay.Services
{
    public class InputService : ITickable
    {
        private enum SwipeDirection
        {
            Invalid = 0,
            Up = 1,
            Down = 2,
            Left = 3,
            Right = 4
        }

        private const float SwipeThreshold = 50f;
        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;

        public event Action InvokedSwipe;
        public event Action<int> Swiped; 
        public event Action InvokedUp;
        
        public void Tick()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startTouchPosition = touch.position;
                        break;

                    case TouchPhase.Ended:
                        _endTouchPosition = touch.position;
                        if (DetectSwipe(out var direction))
                        {
                            switch (direction)
                            {
                                case SwipeDirection.Invalid:
                                    break;
                                case SwipeDirection.Up:
                                    break;
                                case SwipeDirection.Down:
                                    break;
                                case SwipeDirection.Left:
                                    break;
                                case SwipeDirection.Right:
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                            Swiped?.Invoke((int)direction);
                        }
                        else
                        {
                            PointUp();
                        }
                        break;
                }
            }
        }

        private void Swapping()
        {
           
        }

        private void PointUp()
        {
            InvokedUp?.Invoke();
            Debug.Log("Point up");
        }

        private bool DetectSwipe(out SwipeDirection direction)
        {
            Vector2 swipeDelta = _endTouchPosition - _startTouchPosition;
            direction = SwipeDirection.Invalid;

            if (swipeDelta.magnitude > SwipeThreshold)
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                    {
                        Debug.Log("Right Swipe");
                        direction = SwipeDirection.Right;
                    }
                    else
                    {
                        Debug.Log("Left Swipe");
                        direction = SwipeDirection.Left;
                    }
                }
                else
                {
                    if (swipeDelta.y > 0)
                    {
                        Debug.Log("Up Swipe");
                        direction = SwipeDirection.Up;
                    }
                    else
                    {
                        Debug.Log("Down Swipe");
                        direction = SwipeDirection.Down;
                    }
                }
            }

            return direction != SwipeDirection.Invalid;
        }
    }
}