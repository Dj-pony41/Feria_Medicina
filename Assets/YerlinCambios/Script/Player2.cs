using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
	public float moveSpeed = 5f; 
	private Camera mainCamera; 
	void Start()
	{
		mainCamera = Camera.main; 	}

	void Update()
	{
		MovePlayer();
		RotateTowardsMouse();
	}

	void MovePlayer()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");

		Vector3 move = new Vector3(moveX, 0, moveZ);
		move = mainCamera.transform.TransformDirection(move);
		move.y = 0; 
		transform.Translate(move.normalized * moveSpeed * Time.deltaTime, Space.World);
	}

	void RotateTowardsMouse()
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			Vector3 targetPosition = hit.point;
			targetPosition.y = transform.position.y; 
			transform.LookAt(targetPosition);
		}
	}
}
