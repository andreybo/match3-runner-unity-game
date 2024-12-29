using MOSoft.SwipeController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class GameControllerButton : MonoBehaviour
{
	public float gravity = 20.0f;
	float jumpHeight = 3f;

	[SerializeField] GameObject worldParent;

	public int line;
	Rigidbody _rb;
	float hh;
	bool ifCombo;
	bool isJumped = false;
	bool isCombo = false;
	//bool isMoving = true;

	public bool isLeft = false;
	public bool isRight = false;

	Vector2 touchStartPos = Vector2.zero;
	bool ifHit = false;
	public float speed;
    public float levelSpeed;
    public bool speedUp;
    public static GameControllerButton Instance;
    
    void Awake(){
        Instance = this;
    }

	private void Start()
	{	
		ifCombo = PlayerPrefs.GetInt("combo") == 0 ? true : false;
		_rb = GetComponent<Rigidbody>();
		_rb.constraints = RigidbodyConstraints.FreezeRotation;
		_rb.freezeRotation = true;
		_rb.useGravity = false;
		StartGame();
        speed = PlayerPrefs.GetFloat("speed");
        levelSpeed = speed;
	}

	public void StartGame()
    {
		line = 0;
		jumpHeight = PlayerPrefs.GetFloat("height");
		hh = CalculateJumpVerticalSpeed();
	}


	public void Combo()
	{
		if(!isCombo){
			isCombo = true;
			_rb.velocity = new Vector3(_rb.velocity.x, -12f, _rb.velocity.z);
		}
		Debug.Log("Тык SlideDown");
	}

	
	public void JumpUp(){
		if(!isJumped){
			isJumped = true;
			_rb.velocity = new Vector3(_rb.velocity.x, 8f, _rb.velocity.z);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Jumps();
		isJumped = false;
		isCombo = false;
		Ball.Instance.OnColl();
		if (collision.gameObject.CompareTag("Cube"))
		{
			string cube = collision.collider.gameObject.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
			if (cube == Ball.Instance.colorName)
			{
				GameStatus.Instance.coinsPlay(transform.position);
				GameStatus.Instance.AddToScore(1);
				collision.gameObject.SetActive(false);        
				Character.Instance.CoinsSound();
				Ray(collision.gameObject.transform);
			}
		}
		else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall")){
			ifHit = true;
			speed = -5;
		}
	}
	


    public void Ray(Transform point)
    {
        Vector3[] directions = { point.TransformDirection(Vector3.forward), 
                             point.TransformDirection(Vector3.left), 
                             point.TransformDirection(Vector3.right), 
                             point.TransformDirection(Vector3.back) };
        RaycastHit hit;

        foreach (Vector3 dir in directions)
        {
            if (Physics.Raycast(transform.position, dir, out hit, 1f))
            {
                if (hit.collider.gameObject.CompareTag("Cube"))
                {
                    Cube cube = hit.collider.gameObject.GetComponent<Cube>();
					string cubename = hit.collider.gameObject.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                    if (cubename == Ball.Instance.colorName)
                    {
                        GameStatus.Instance.AddToScore(1);
                        GameStatus.Instance.coinsPlay(transform.position);
                        cube.PlayBlockDestroy();
                    }
                }
            }
        }
    }

	public void Jumps()
	{
		_rb.velocity = new Vector3(_rb.velocity.x, hh, _rb.velocity.z);		
        Character.Instance.JumpSound();
	}

	void FixedUpdate()
	{
		_rb.AddForce(new Vector3(0, -gravity * _rb.mass, 0));
	}

	float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt((jumpHeight - 1f) * gravity);
	}

	private void Update()
	{
		HandleMovement();
		HandleComboCheck();
	}

	private void HandleMovement()
	{
		Vector3 forwardMovement = Vector3.zero;
		Vector3 lateralMovement = Vector3.zero;

		if (VariableJoystick.Instance.Horizontal != 0)
		{
			lateralMovement = Vector3.right * 3 * VariableJoystick.Instance.Horizontal;
		}

		if (VariableJoystick.Instance.Vertical > 0)
		{
			forwardMovement = Vector3.forward * GameControllerButton.Instance.speed;
		}

		_rb.MovePosition(_rb.position + new Vector3(lateralMovement.x, _rb.velocity.y, forwardMovement.z) * Time.deltaTime);

		//Debug.Log("Forward Movement: " + forwardMovement + " | Lateral Movement: " + lateralMovement);
	}

	

	private void HandleComboCheck()
	{
		if (ifCombo && transform.position.y > 2 &&Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 6))
		{
			if (hit.collider.gameObject.CompareTag("Cube"))
			{
				string cube = hit.collider.gameObject.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
				
				if (cube == Ball.Instance.colorName)
				{
						Combo();
				}
			}
		}
	}

}