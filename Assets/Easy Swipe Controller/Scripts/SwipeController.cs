using UnityEngine;
using System.Collections;


namespace MOSoft.SwipeController
{
    public class SwipeController : MonoBehaviour
    {
        // Min length to detect the Swipe
        public float m_minSwipeLength = 35f;

        // Movement when touch and hold the screen or mouse button down
        public bool m_continuousMovment = false;

        // Should debug messages be printed to Debug.log
        public bool m_debugEnabled = false;

        public bool m_isPlaymakerSupported = false;

        private bool m_isDoubleTap = false;
        private bool m_isLeftTap = false;
        private bool m_isRightTap = false;

        private Vector2 m_firstPressPos;
        private Vector2 m_secondPressPos;
        private Vector2 m_currentSwipe;
        private Vector2 m_firstClickPos;
        private Vector2 m_secondClickPos;

        private ESwipeDirection m_swipeDirection = ESwipeDirection.None;
        private ESwipeDirection m_lastDirection = ESwipeDirection.None;

        private bool m_isHoldActive = false;

        private static SwipeController s_myInstance;
        private float width;

        private void Awake()
        {
            width = Screen.width / 2;
            Debug.Log("swScreenWidth: " + width);
        }

        public static SwipeController getInstance
        {
            get
            {
                if (s_myInstance == null)
                {
                    s_myInstance = GameObject.FindObjectOfType<SwipeController>();
                }
                return s_myInstance;
            }
        }

        private void Update()
        {
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            //TOUCH
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                
                if (t.phase == TouchPhase.Began)
                {
                    if (!m_isHoldActive)
                    {
                        m_isHoldActive = true;
                        m_firstPressPos = new Vector2(t.position.x, t.position.y);
                        if (m_firstPressPos.x < width)
                        {
                            m_isLeftTap = true;
                            m_isRightTap = false;
                            Debug.Log("swTapPos: " + m_firstPressPos.x);
                        }
                        else if (m_firstPressPos.x > width)
                        {
                            m_isLeftTap = false;
                            m_isRightTap = true;
                            Debug.Log("swTapPos: " + m_firstPressPos.x);
                        }
                    }
                }

                if (m_continuousMovment && m_isHoldActive && t.phase == TouchPhase.Moved && (m_firstClickPos.x != t.position.x || m_firstClickPos.y != t.position.y))
                {
                    handleTouchMovement(t);
                }

                if (!m_continuousMovment && t.phase == TouchPhase.Ended)
                {
                    handleTouchMovement(t);
                }

                m_isDoubleTap = t.tapCount == 2;// m_lastDirection.Equals(ESwipeDirection.Tap) && ((Time.time - m_lastTapTime) < m_doubleClickSpeedInMilliseconds);
            }
            //MOUSE
            else
            {
                //Mouse detection part for simulating swipe control
                if (Input.GetMouseButton(0))
                {
                    if (!m_isHoldActive)
                    {
                        m_isHoldActive = true;
                        m_firstClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    }
                }
                else
                {
                    m_isHoldActive = false;
                    m_swipeDirection = ESwipeDirection.None;
                }

                //the alternative way with hold
                if (m_continuousMovment && m_isHoldActive && (m_firstClickPos.x != Input.mousePosition.x || m_firstClickPos.y != Input.mousePosition.y))
                {
                    handleMouseMovement();
                }

                if (!m_continuousMovment && Input.GetMouseButtonUp(0))
                {
                    //mouse button released
                    handleMouseMovement();
                }

                m_isDoubleTap = Input.GetMouseButtonUp(1);
            }
        }

        private void handleTouchMovement(Touch t)
        {
            m_secondPressPos = new Vector2(t.position.x, t.position.y);
            m_currentSwipe = new Vector3(m_secondPressPos.x - m_firstPressPos.x, m_secondPressPos.y - m_firstPressPos.y);

            //save the last direction
            writeDebug("Last Touch: " + m_swipeDirection);
            m_lastDirection = m_swipeDirection;

            writeDebug("Swipe length: " + m_currentSwipe.magnitude);
            // Make sure it was a real swipe and not a single tap
            if (m_currentSwipe.magnitude < m_minSwipeLength)
            {
                m_swipeDirection = ESwipeDirection.Tap;                                
                sendEvent("TapEvent");                
                writeDebug("Tap");                
                return;
            }

            m_currentSwipe.Normalize();

            //Swipe direction checks

            // Swipe up
            if (m_currentSwipe.y > 0 && m_currentSwipe.x > -0.5f && m_currentSwipe.x < 0.5f)
            {
                m_swipeDirection = ESwipeDirection.Up;
                sendEvent("UpEvent");                
                writeDebug("Up");
            }
            // Swipe down
            else if (m_currentSwipe.y < 0 && m_currentSwipe.x > -0.5f && m_currentSwipe.x < 0.5f)
            {
                m_swipeDirection = ESwipeDirection.Down;
                sendEvent("DownEvent");                
                writeDebug("Down");
            }
            // Swipe left
            else if (m_currentSwipe.x < 0 && m_currentSwipe.y > -0.5f && m_currentSwipe.y < 0.5f)
            {
                m_swipeDirection = ESwipeDirection.Left;
                sendEvent("LeftEvent");                
                writeDebug("Left");
            }
            // Swipe right
            else if (m_currentSwipe.x > 0 && m_currentSwipe.y > -0.5f && m_currentSwipe.y < 0.5f)
            {
                m_swipeDirection = ESwipeDirection.Right;
                sendEvent("RightEvent");                
                writeDebug("Right");
            }
            // Swipe up left
            else if (m_currentSwipe.y > 0 && m_currentSwipe.x < 0)
            {
                m_swipeDirection = ESwipeDirection.UpLeft;
                writeDebug("UpLeft");
            }
            // Swipe up right
            else if (m_currentSwipe.y > 0 && m_currentSwipe.x > 0)
            {
                m_swipeDirection = ESwipeDirection.UpRight;
                writeDebug("UpRight");
            }
            // Swipe down left
            else if (m_currentSwipe.y < 0 && m_currentSwipe.x < 0)
            {
                m_swipeDirection = ESwipeDirection.DownLeft;
                writeDebug("DownLeft");
            }
            // Swipe down right
            else if (m_currentSwipe.y < 0 && m_currentSwipe.x > 0)
            {
                m_swipeDirection = ESwipeDirection.DownRight;
                writeDebug("DownRight");
            }
        }

        private void handleMouseMovement()
        {
            m_secondClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            m_currentSwipe = new Vector3(m_secondClickPos.x - m_firstClickPos.x, m_secondClickPos.y - m_firstClickPos.y);

            //Save last mouse movement
            writeDebug("Last mouse movement: " + m_swipeDirection);
            m_lastDirection = m_swipeDirection;
            
            // Make sure it was a real swipe and not a single tap
            if (m_currentSwipe.magnitude < m_minSwipeLength)
            {
                m_swipeDirection = ESwipeDirection.Tap;                
                sendEvent("TapEvent");              
                writeDebug("Tap");
                return;
            }
            m_currentSwipe.Normalize();

            //Swipe direction checks

            // Swipe up
            if (m_currentSwipe.y > 0 && m_currentSwipe.x > -0.5f && m_currentSwipe.x < 0.5f)
            {
                m_swipeDirection = ESwipeDirection.Up;
                sendEvent("UpEvent");                
                writeDebug("Up");
            }
            // Swipe down
            else if (m_currentSwipe.y < 0 && m_currentSwipe.x > -0.5f && m_currentSwipe.x < 0.5f)
            {
                m_swipeDirection = ESwipeDirection.Down;
                sendEvent("DownEvent");                
                writeDebug("Down");
            }
            // Swipe left
            else if (m_currentSwipe.x < 0 && m_currentSwipe.y > -0.5f && m_currentSwipe.y < 0.5f)
            {
                m_swipeDirection = ESwipeDirection.Left;
                sendEvent("LeftEvent");                
                writeDebug("Left");
            }
            // Swipe right
            else if (m_currentSwipe.x > 0 && m_currentSwipe.y > -0.5f && m_currentSwipe.y < 0.5f)
            {
                m_swipeDirection = ESwipeDirection.Right;
                sendEvent("RightEvent");                
                writeDebug("Right");
            }     // Swipe up left
            else if (m_currentSwipe.y > 0 && m_currentSwipe.x < 0)
            {
                m_swipeDirection = ESwipeDirection.UpLeft;
                sendEvent("UpLeftEvent");
                writeDebug("UpLeft");
            }
            // Swipe up right
            else if (m_currentSwipe.y > 0 && m_currentSwipe.x > 0)
            {
                m_swipeDirection = ESwipeDirection.UpRight;
                sendEvent("UpRightEvent");
                writeDebug("UpRight");
            }
            // Swipe down left
            else if (m_currentSwipe.y < 0 && m_currentSwipe.x < 0)
            {
                m_swipeDirection = ESwipeDirection.DownLeft;
                sendEvent("DownLeftEvent");
                writeDebug("DownLeft");
            }
            // Swipe down right
            else if (m_currentSwipe.y < 0 && m_currentSwipe.x > 0)
            {
                m_swipeDirection = ESwipeDirection.DownRight;
                sendEvent("DownRightEvent");
                writeDebug("DownRight");
            }
        }

        public virtual void sendEvent(string evt)
        {

        }

        // write debug output if enabled
        private void writeDebug(string message)
        {
            if (!m_debugEnabled)
            {
                return;
            }
            Debug.Log(message);
        }

        // ### Public control methods to check swipe direction ###
        public bool isTap()
        {
            return m_swipeDirection.Equals(ESwipeDirection.Tap);
        }

        public bool isUp()
        {
            if (m_continuousMovment)
            {
                return m_isHoldActive && m_swipeDirection.Equals(ESwipeDirection.Up);
            }
            return m_swipeDirection.Equals(ESwipeDirection.Up);
        }

        public bool isDown()
        {
            if (m_continuousMovment)
            {
                return m_isHoldActive && m_swipeDirection.Equals(ESwipeDirection.Down);
            }
            return m_swipeDirection.Equals(ESwipeDirection.Down);
        }

        public bool isLeft()
        {
            if (m_continuousMovment)
            {
                return m_isHoldActive && m_swipeDirection.Equals(ESwipeDirection.Left);
            }
            return m_swipeDirection.Equals(ESwipeDirection.Left);
        }

        public bool isRight()
        {
            if (m_continuousMovment)
            {
                return m_isHoldActive && m_swipeDirection.Equals(ESwipeDirection.Right);
            }
            return m_swipeDirection.Equals(ESwipeDirection.Right);
        }

        public bool isUpLeft()
        {
            if (m_continuousMovment)
            {
                return m_isHoldActive && m_swipeDirection.Equals(ESwipeDirection.UpLeft);
            }
            return m_swipeDirection.Equals(ESwipeDirection.UpLeft);
        }

        public bool isUpRight()
        {
            if (m_continuousMovment)
            {
                return m_isHoldActive && m_swipeDirection.Equals(ESwipeDirection.UpRight);
            }
            return m_swipeDirection.Equals(ESwipeDirection.UpRight);
        }

        public bool isDownLeft()
        {
            if (m_continuousMovment)
            {
                return m_isHoldActive && m_swipeDirection.Equals(ESwipeDirection.DownLeft);
            }
            return m_swipeDirection.Equals(ESwipeDirection.DownLeft);
        }

        public bool isDownRight()
        {
            if (m_continuousMovment)
            {
                return m_isHoldActive && m_swipeDirection.Equals(ESwipeDirection.DownRight);
            }
            return m_swipeDirection.Equals(ESwipeDirection.DownRight);
        }

        /**
         * Returns the last swipe direction.
         */
        public ESwipeDirection getLastDirection()
        {
            return m_lastDirection;
        }

        /**
         * Movement when touch and hold the screen or mouse button down
         */
        public bool isContinuousMovment()
        {
            return m_continuousMovment;
        }

        /**
         * Double tap or right mouse button up
         */
        public bool isDoubleTap()
        {
            return m_isDoubleTap;
        }

        public bool isTapLeft()
        {
            return m_isLeftTap;
        }

        public bool isTapRight()
        {
            return m_isRightTap;
        }

    }
}