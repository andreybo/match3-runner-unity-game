using UnityEngine;
using System.Collections;

namespace MOSoft.SwipeController
{
    // Values for movement direction
    public enum ESwipeDirection
    {
        None, // Idle
        Tap, // Single short touch
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    };
}