using MOSoft.SwipeController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour
{
	public float gravity = 20.0f;
	float jumpHeight = 4f;

	[SerializeField] AudioSource maudio;
	[SerializeField] TrailRenderer trail;
	[SerializeField] AudioClip swipe;
	[SerializeField] GameObject worldParent;
	public int lineX_back;
	public int lineZ_back;

	public int line;
	public int z;
	Rigidbody _rb;
	Vector3 target;
	public bool move;
	float hh;
	bool ifCombo;

	private void Start()
	{
		ifCombo = PlayerPrefs.GetInt("combo") == 0 ? false : true;
		_rb = GetComponent<Rigidbody>();
		_rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		_rb.freezeRotation = true;
		_rb.useGravity = false;
		StartGame();
	}

	public void StartGame()
	{
		line = 0;
		z = 0;
		lineX_back = 0;
		lineZ_back = 0;
		jumpHeight = PlayerPrefs.GetFloat("height");
		hh = CalculateJumpVerticalSpeed();
	}


	public void Combo()
	{
		Debug.Log("SlideDown");
		_rb.velocity = new Vector3(_rb.velocity.x, -25, _rb.velocity.z);
		//Ball.Instance.isCombo = true;
		//grounded = false;
		//maudio.Play();
	}

	private void OnCollisionEnter(Collision collision)
	{
		//grounded = true;
		Jumps();
		Ball.Instance.OnColl();
	}

	private void OnCollisionExit(Collision collision)
	{
		//Ball.Instance.hasStarted = false;
		//GameStatus.Instance.ifComboColor = false;
		//grounded = false;
	}

	public void Jumps()
	{
		_rb.velocity = new Vector3(_rb.velocity.x, hh, _rb.velocity.z);
		//Debug.Log("jump" + hh);
		//_rb.velocity = Vector3.up * hh;
	}

	void FixedUpdate()
	{
		// We apply gravity manually for more tuning control
		_rb.AddForce(new Vector3(0, -gravity * _rb.mass, 0));
		RaycastHit hit;
		if (ifCombo & Physics.Raycast(transform.position, -transform.up, out hit, 6))
		{
			if (hit.collider.gameObject.CompareTag("Cube"))
			{
				Cube Cube = hit.collider.gameObject.GetComponent<Cube>();
				if (Cube.colorName == Ball.Instance.colorName)
				{
					Combo();
				}
			}
		}
		//grounded = false;
	}
	/*void OnCollisionStay()
	{
		grounded = true;
	}*/

	float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(jumpHeight * gravity);
	}

	private void Update()
	{
		//_rb.AddForce(new Vector3(0, -gravity * _rb.mass, 0));

		//Debug.Log("jump " + target);
		if (move)
		{
			worldParent.transform.position = Vector3.MoveTowards(worldParent.transform.position, target, 10 * Time.smoothDeltaTime);
			Debug.Log("target " + target);
			if (worldParent.transform.position.z == z)
			{
				move = false;
			}
		}
	}

	public void moveLeft()
	{
		lineZ_back = z;
		lineX_back = line;
		line -= 1;
		z -= 1;
		target = new Vector3(line, 0, z);
		move = true;
		//trail.enabled = true;
	}
	public void moveRight()
	{
		lineZ_back = z;
		lineX_back = line;
		line += 1;
		z -= 1;
		target = new Vector3(line, 0, z);
		move = true;
		//trail.enabled = true;
	}
	public void moveUp()
	{
		lineZ_back = z;
		lineX_back = line;
		line += 0;
		z -= 1;
		target = new Vector3(line, 0, z);
		move = true;
		//trail.enabled = true;
	}

	public void BackPosition()
	{
		line = lineX_back;
		z = lineZ_back;
		target = new Vector3(line, 0, z);
		move = true;
	}
}
