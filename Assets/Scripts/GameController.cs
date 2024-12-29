using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject Din;
	float lerpDuration = 0.4f;
	private float width;
    private float moveSpeed = 6f;
    public float speed = 1;

    public float rotSpeed = 200f;
    float z = 0;
    public float x = 0;
	//bool isMoving;
	Character character;
	public int line;
	//bool isTap;
	public float timeTouchperiod = 0.2f;
	public int TapCount;
	bool isCount;
	bool isTouched;


	protected Vector2 m_StartingTouch;
	protected Vector2 m_EndingTouch;

	protected bool m_IsInvincible;
	protected bool m_IsRunning;

	protected float m_JumpStart;
	protected bool m_Jumping;

	protected bool m_Sliding;
	protected float m_SlideStart;
	protected bool m_IsSwiping = false;


    private void Start()
    {
		width = Screen.width / 2.0f;
		character = GetComponent<Character>();
	}

    void Update()
	{
		// Use touch input on mobile
		if (Input.touchCount > 0)
		{
			//isTap = true;
			Touch touch = Input.GetTouch(0);

			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Debug.Log("Tap1Began");
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				Debug.Log("Tap1End");
				//StartCoroutine(MoveEnd());
			}


			//Swipes
			if (Input.touchCount == 1)
			{
				if (m_IsSwiping)
				{
					Vector2 diff = Input.GetTouch(0).position - m_StartingTouch;

					// Put difference in Screen ratio, but using only width, so the ratio is the same on both
					// axes (otherwise we would have to swipe more vertically...)
					diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

					if (diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
					{
						if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
						{
							if (diff.y < 0)
							{
								Debug.Log("TapSlideDown");
								Din.GetComponent<Rigidbody>().velocity = new Vector3(0, -10, 0);
								//Ball.Instance.isCombo = true;
								Debug.Log("down swipe");
								//Slide();
							}
							else
							{
								Debug.Log("TapSlideUp");
								Din.GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
								//Ball.Instance.isCombo = true;
								Debug.Log("down swipe");
								//Slide();
							}
						}
						else
						{
							if (diff.x < 0)
							{
								StartCoroutine(MoveEnd(-1));
								Debug.Log("TapL");
							}
							else
							{
								StartCoroutine(MoveEnd(1));
								Debug.Log("TapL");
							}
						}

						m_IsSwiping = false;
					}
				}

				// Input check is AFTER the swip test, that way if TouchPhase.Ended happen a single frame after the Began Phase
				// a swipe can still be registered (otherwise, m_IsSwiping will be set to false and the test wouldn't happen for that began-Ended pair)
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					Debug.Log("TapBegan");
					m_StartingTouch = Input.GetTouch(0).position;
					m_IsSwiping = true;
				}
				else if (Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					Debug.Log("TapEnd");
					m_IsSwiping = false;
					m_EndingTouch = Input.GetTouch(0).position;
				}
			}


			if (m_Sliding)
			{
				// Slide time isn't constant but the slide length is (even if slightly modified by speed, to slide slightly further when faster).
				// This is for gameplay reason, we don't want the character to drasticly slide farther when at max speed.
				//float correctSlideLength = slideLength * (1.0f + trackManager.speedRatio);
				float ratio = Vector2.Distance(m_StartingTouch, m_EndingTouch);
				if (ratio >= 1.0f)
				{
					// We slid to (or past) the required length, go back to running
					//StopSliding();
					Debug.Log("TapSlideEnd");
					m_Sliding = false;
				}
			}

			/*if (m_Jumping)
			{
				if (isMoving)
				{
					// Same as with the sliding, we want a fixed jump LENGTH not fixed jump TIME. Also, just as with sliding,
					// we slightly modify length with speed to make it more playable.
					float ratio = Vector2.Distance(m_StartingTouch, m_EndingTouch);
					if (ratio >= 1.0f)
					{
						Debug.Log("TapJumpEnd");
						m_Jumping = false;
						//character.animator.SetBool(s_JumpingHash, false);
					}
					else
					{
						Debug.Log("TapJumpStar");
						//verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
					}
				}
			}*/
		}

	}

    private void FixedUpdate()
	{
		z = Din.transform.position.z + speed * Time.deltaTime;
		Din.transform.position = new Vector3(x, Din.transform.position.y, z);
	}


    void MovePaddle(int direction)
	{
		x = transform.position.x + (direction * Time.deltaTime * moveSpeed);
		line = Mathf.RoundToInt(x);
	}

	IEnumerator MoveEnd(int i)
	{
		float timeElapsed = 0;
		line += i;
		//isMoving = true;
		while (timeElapsed < lerpDuration)
		{
			x = Mathf.Lerp(x, line, timeElapsed / lerpDuration);
			timeElapsed += Time.deltaTime;

			yield return null;
		}

		x = line;
		//isMoving = false;
	}


}
