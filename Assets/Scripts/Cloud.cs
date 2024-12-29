using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	float maxDistance = 4;
	float minDistance = -4;
	int direction = 1;
	float speed = 5;
	//[SerializeField] Animator fAnim;
	float secondsBetweenSpawn = 3;
	float elapsedTime;
	//int pickAnumber = 0;
	float distance;
	Vector3 dir;
	public bool isCube = false;
	public Vector3 whereCube;

	private void Start()
    {
		getWall(1);
		distance = 7f;
		dir = new Vector3(0, -1);
	}
	void Elapsed()
	{
		secondsBetweenSpawn = Random.Range(3, 7);
		//fAnim.SetInteger("randFace", Random.Range(1, 4));
		Debug.Log("Elapsed");
	}

	void Update()
	{
		CheckRaycast();
		MoveObject();

	}

	void CheckRaycast()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, dir, out hit, distance))
		{
			isCube = true;
			whereCube = new Vector3(hit.transform.position.x, transform.position.y, transform.position.z);
		}
		else
		{
			isCube = false;
		}
	}

	void MoveObject()
	{
		transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

		if (transform.position.x >= maxDistance)
		{
			transform.position = new Vector3(maxDistance, transform.position.y, transform.position.z);
			ChangeDirection();
		}
		else if (transform.position.x <= minDistance)
		{
			transform.position = new Vector3(minDistance, transform.position.y, transform.position.z);
			ChangeDirection();
		}
	}

	void ChangeDirection()
	{
		direction = -direction;
	}

	public void getWall(float i)
    {
		maxDistance = 4;
		minDistance = -4;
	}

}
